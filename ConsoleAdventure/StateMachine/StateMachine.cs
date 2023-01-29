//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public class StateMachine
{
    public IState CurrentState { get; private set; }
    public string CurrentStateName => CurrentState == null ? "NONE" : CurrentState.GetType().Name;
   
    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type,List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();
    private List<Transition> _anyTransitions = new List<Transition>();
   
    private static List<Transition> emptyTransitions = new List<Transition>(0);


    public void Tick()
    {
        Transition transition = GetTransition();
        if (transition != null) SetState(transition.To);
            
        CurrentState?.Tick();
        
    }

    public void SetState(IState state, bool callOnEnter = true)
    {
        if (state == CurrentState)
            return;
      
        CurrentState?.OnExit();
        CurrentState = state;
      
        _transitions.TryGetValue(CurrentState.GetType(), out _currentTransitions);
        if (_currentTransitions == null)
            _currentTransitions = emptyTransitions;

        if (callOnEnter) CurrentState.OnEnter();
        //if we return to this state instead of starting a new one
        else Console.WriteLine(CurrentState.TextWhenReturningToThisState);
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (_transitions.TryGetValue(from.GetType(), out List<Transition> transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }
      
        transitions.Add(new Transition(to, predicate));
    }
    
    public void AddTransition(IState from, IState to, ref Action trigger)
    {
        if (_transitions.TryGetValue(from.GetType(), out List<Transition> transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }
        
        transitions.Add(new Transition(to, ref trigger));
    }

    public void AddAnyTransition(IState state, Func<bool> predicate)
    {
        _anyTransitions.Add(new Transition(state, predicate));
    }
    
    public void AddAnyTransition(IState state, ref Action trigger)
    {
        _anyTransitions.Add(new Transition(state, ref trigger));
    }

    private class Transition
    {
        public Func<bool> Condition {get; }
        public IState To { get; }

        private bool Triggered() => _triggered;
        private bool _triggered;
        public void ResetTrigger() => _triggered = false;
        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
        
        public Transition(IState to, ref Action trigger)
        {
            To = to;
            trigger += OnTrigger;
            Condition = Triggered;
        }

        private void OnTrigger()
        {
            _triggered = true;
        }
    }

    private Transition GetTransition()
    {
        foreach (Transition transition in _anyTransitions)
        {
            if (transition.Condition())
            {
                transition.ResetTrigger();
                return transition;
            }
        }
        
        foreach (Transition transition in _currentTransitions)
        {
            if (transition.Condition())
            {
                transition.ResetTrigger();
                return transition;
            }
        }

        return null;
    }
}