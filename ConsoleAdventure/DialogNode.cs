//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public class DialogNode
{
    public DialogNode Parent { get; private set; }
    
    public DialogNode[] Children { get; private set; }

    public string Keyword { get; private set; }
    
    public string DialogText { get; private set; }

    public DialogNode(DialogNode parent, DialogNode[] children, string keyword, string dialogText)
    {
        Parent = parent;
        Children = children;
        Keyword = keyword;
        DialogText = dialogText;
    }
    
    public override string ToString() => DialogText;
}