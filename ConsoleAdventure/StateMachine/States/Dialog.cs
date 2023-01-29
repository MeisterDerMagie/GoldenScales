//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public class Dialog : IState
{
    public List<Command> AvailableCommands { get; set; }
    public string TextWhenReturningToThisState => "You continue your conversation with NPC-NAME.";

    public Dialog()
    {
        AvailableCommands = new List<Command>();
    }
    
    public void OnEnter()
    {
        throw new NotImplementedException();
    }

    public void Tick()
    {
        throw new NotImplementedException();
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

}