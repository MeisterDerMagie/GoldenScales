//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items;

public abstract class Item : IInteractable
{
    public string Name { get; }
    public bool Price { get; }
    public bool Equippable { get; }
    public bool Consumable { get; }

    public string Keyword => Name;
    public string[] Interactions { get; }
    
    protected Item(bool equippable, bool consumable, bool price, string[] interactions, string name)
    {
        Equippable = equippable;
        Consumable = consumable;
        Price = price;
        Interactions = interactions;
        Name = name;
    }

    public abstract void Interact(string userInput);
}