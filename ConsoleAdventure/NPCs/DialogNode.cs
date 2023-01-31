//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.NPCs;

public class DialogNode
{
    public DialogNode Parent { get; set; }

    public List<DialogNode> Children = new();

    public string Option { get; }

    private string Answer { get; }
    private Action EnterAction { get; }

    public DialogNode EnterChild(int index)
    {
        if (index > Children.Count - 1)
        {
            Console.WriteLine($"ERROR: Can't enter dialog node child of index {index}. There are only {Children.Count} children.");
            return null;
        }
        
        Children[index].Enter();
        return Children[index];
    }
    
    public void Enter()
    {
        Console.Write($"\"{Answer}\"\n");
        DisplayOptions();
        EnterAction?.Invoke();
    }
    
    private void DisplayOptions()
    {
        for (int i = 0; i < Children.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {Children[i].Option}");
        }
    }

    public DialogNode(string option, string answer, DialogNode parent, Action enterAction = null)
    {
        Option = option;
        Answer = answer;
        Parent = parent;
        EnterAction = enterAction;
    }

    public DialogNode AddChild(string option, string answer, Action enterAction = null)
    {
        var child = new DialogNode(option, answer, this, enterAction);
        Children.Add(child);
        return child;
    }
}