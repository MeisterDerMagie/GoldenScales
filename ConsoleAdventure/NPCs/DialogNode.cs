//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.NPCs;

public class DialogNode
{
    public DialogNode Parent { get; }

    public List<DialogNode> Children = new();

    public string Option { get; }

    public string Answer { get; }

    public DialogNode(string option, string answer, DialogNode parent)
    {
        Option = option;
        Answer = answer;
        Parent = parent;
    }

    public DialogNode AddChild(string option, string answer)
    {
        var child = new DialogNode(option, answer, this);
        Children.Add(child);
        return child;
    }
}