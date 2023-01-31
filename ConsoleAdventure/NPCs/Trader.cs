//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;

namespace ConsoleAdventure.NPCs;

public class Trader : NPC
{
    public List<Item> Inventory { get; }

    public Trader(string name, DialogNode dialog, List<Item> inventory) : base(name, dialog)
    {
        Inventory = inventory;
    }

    public Trader(string name, List<Item> inventory) : base(name)
    {
        Inventory = inventory;
    }
}