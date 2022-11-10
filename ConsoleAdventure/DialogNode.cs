//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public class DialogNode
{
    public DialogNode Parent { get; private set; }
    
    public DialogNode[] Children { get; private set; }

    public string Keyword { get; private set; }
    
    public string DialogText { get; private set; }

    public DialogNode(DialogNode _parent, DialogNode[] _children, string _keyword, string _dialogText)
    {
        Parent = _parent;
        Children = _children;
        Keyword = _keyword;
        DialogText = _dialogText;
    }
    
    public override string ToString() => DialogText;
}