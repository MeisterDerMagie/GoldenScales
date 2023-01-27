//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public class SearchInventory : IState
{
    public List<Command> AvailableCommands { get; set; }

    public SearchInventory()
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