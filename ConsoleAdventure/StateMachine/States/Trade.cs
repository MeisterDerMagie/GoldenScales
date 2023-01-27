//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public class Trade : IState
{
    public List<Command> AvailableCommands { get; set; }

    public Trade()
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