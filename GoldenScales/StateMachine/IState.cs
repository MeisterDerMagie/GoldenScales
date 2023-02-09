//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public interface IState
{
    public List<Command> AvailableCommands { get; set; }
    public string TextWhenReturningToThisState { get; }
    
    public void OnEnter();
    public void Tick();
    public void OnExit();
}