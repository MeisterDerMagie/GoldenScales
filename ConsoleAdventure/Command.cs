//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Command
{
    public string Name;
    public bool Hidden; //if true, the command will not be displayed when all available commands are listed
    private readonly List<string> _keywords = new();
    private Action<List<string>> _actionWithParameters;
    private Action _actionWithoutParameter;
    private bool _hasParameters;

    public Command(string name, List<string> keywords, Action<List<string>> action, bool hidden = false)
    {
        Name = name;
        _keywords = keywords;
        _actionWithParameters = action;
        _actionWithoutParameter = null;
        _hasParameters = true;
        Hidden = hidden;
    }

    public Command(string name, List<string> keywords, Action action, bool hidden = false)
    {
        Name = name;
        _keywords = keywords;
        _actionWithParameters = null;
        _actionWithoutParameter = action;
        _hasParameters = false;
        Hidden = hidden;
    }
    
    public bool ContainsKeyword(string userInput)
    {
        foreach (string keyword in _keywords)
        {
            if (userInput.Contains(keyword)) return true;
        }

        return false;
    }

    public string RemoveCommandKeyword(string userInput)
    {
        foreach (string keyword in _keywords)
        {
            if (userInput.Contains(keyword)) return userInput.RemoveFirstOccurence(keyword);
        }

        return userInput;
    }

    public void Execute(List<string> parameters)
    {
        if(_hasParameters)
            _actionWithParameters?.Invoke(parameters);
        else
            _actionWithoutParameter?.Invoke();
    }
}