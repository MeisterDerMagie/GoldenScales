//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.NPCs;

public class NPC
{
    public string Name { get; }
    public DialogNode Dialog { get; private set; }

    public NPC(string name, DialogNode dialog)
    {
        Name = name;
        Dialog = dialog;
    }

    public NPC(string name)
    {
        Name = name;
    }

    public void SetDialog(DialogNode dialog)
    {
        Dialog = dialog;
    }
}