//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.NPCs;

public class NPC
{
    public string Name { get; }
    public DialogNode Dialog { get; }

    public NPC(string name, DialogNode dialog)
    {
        Name = name;
        Dialog = dialog;
    }
}