//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.NPCs;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Trade : IState
{
    public List<Command> AvailableCommands { get; set; }
    public string TextWhenReturningToThisState => "You continue trading.";

    private Trader _trader;
    
    public Trade(Trader trader)
    {
        _trader = trader;
        AvailableCommands = new List<Command>();
    }


    public void OnEnter()
    {
        throw new NotImplementedException();
    }

    public void Tick()
    {
        UpdateAvailableCommands();
        string userInput = ConsoleUtilities.InputString("What do you want to do trade?");
        if (CommandUtilities.TryExecuteUserInput(userInput, AvailableCommands) == false)
        {
            Console.WriteLine("Type \"options\" to see a list of all actions you can do right now.");
        }
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    private void UpdateAvailableCommands()
    {
        AvailableCommands.Clear();
        
        
    }
}