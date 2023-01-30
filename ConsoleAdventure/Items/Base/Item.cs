//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items;

public abstract class Item
{
    public string Name { get; }
    public int GoldValue { get; }
    public abstract string StatsShort { get; }
    public abstract string StatsFull { get; }
    public bool Equippable => this is Equippable;
    public bool Consumable => this is Consumable;
    
    
    protected Item(string name, int goldValue)
    {
        GoldValue = goldValue;
        Name = name;
    }

    public abstract void Examine();
}