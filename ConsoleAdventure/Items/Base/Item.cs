//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items;

public abstract class Item : IInteractable
{
    public string Name { get; }
    public int Value { get; }
    public bool Equippable { get; }
    public bool Consumable { get; }

    public string Keyword => Name;
    public List<Command> Interactions { get; }
    
    protected Item(bool equippable, bool consumable, int value, List<Command> interactions, string name)
    {
        Equippable = equippable;
        Consumable = consumable;
        Value = value;
        Interactions = interactions;
        Name = name;
        
        //interactions
        Interactions.Add(new Command("Examine", new List<string> {"examine"}, Examine));
    }

    public virtual void Interact(string userInput)
    {
        foreach (Command interaction in Interactions)
        {
            //if(userInput)
        }
    }

    public abstract void Examine();
}