//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.NPCs;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Dialog : IState
{
    public List<Command> AvailableCommands { get; set; }
    public string TextWhenReturningToThisState => "You continue your conversation with NPC-NAME.";

    private NPC _npc;
    private DialogNode _currentNode;

    public Dialog(NPC npc)
    {
        _npc = npc;
        AvailableCommands = new List<Command>();
    }
    
    public void OnEnter()
    {
        _currentNode = _npc.Dialog;
        
        //update available commands
        UpdateAvailableCommands();
        
        //start dialog
        Console.Write($"{_npc.Name}: "); _npc.Dialog.Enter();
    }

    public void Tick()
    {
        UpdateAvailableCommands();
        string prompt = _currentNode.Children.Count == 1
            ? "Type 1 to answer."
            : $"Choose a number between 1 and {_currentNode.Children.Count} to pick your answer.";
        
        string userInput = ConsoleUtilities.InputString(prompt);
        if (CommandUtilities.TryExecuteUserInput(userInput, AvailableCommands) == false)
        {
            Console.WriteLine("Type \"options\" to see a list of all actions you can do right now.");
        }
    }

    public void OnExit()
    {
        
    }
    
    private void UpdateAvailableCommands()
    {
        AvailableCommands.Clear();

        for (int i = 1; i < _currentNode.Children.Count + 1; i++)
        {
            int optionIndex = i;
            AvailableCommands.Add(new Command($"{i} (pick this number to answer with option {i})", new List<string>{$"{i}"}, () => PickOption(optionIndex)));
        }
    }

    private void PickOption(int optionIndex)
    {
        Console.WriteLine($"You: \"{_currentNode.Children[optionIndex - 1].Option}\"");
        Console.Write($"{_npc.Name}: ");
        _currentNode = _currentNode.EnterChild(optionIndex - 1);
    }
}