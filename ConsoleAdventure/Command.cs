//(c) copyright by Martin M. Klöckener
using System.Text.RegularExpressions;
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
        
        //remove empty and duplicate keywords
        _keywords = _keywords.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
        
        //make keywords to lower characters
        _keywords = _keywords.ConvertAll(s => s.ToLower());
    }
    
    public bool ContainsKeyword(string userInput)
    {
        foreach (string keyword in _keywords)
        {
            //if the userInput contains the keyword, return true (only checks for whole words, see: https://stackoverflow.com/questions/31073799/get-only-whole-words-from-a-contains-statement)
            bool foundMatchingKeyword = Regex.Match(userInput, @$"\b{Regex.Escape(keyword)}\b", RegexOptions.IgnoreCase).Success;
            if (foundMatchingKeyword) return true;
        }

        return false;
    }

    public void Execute(List<string> parameters)
    {
        if(_hasParameters)
            _actionWithParameters?.Invoke(parameters);
        else
            _actionWithoutParameter?.Invoke();
    }
}