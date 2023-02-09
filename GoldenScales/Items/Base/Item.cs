//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items;

public abstract class Item
{
    public virtual string Name => _name;
    protected string _name;
    public virtual int GoldValue => _goldValue;
    protected int _goldValue;
    public abstract string StatsShort { get; }
    public abstract string StatsFull { get; }
    public bool Equippable => this is Equippable;
    public bool Consumable => this is Consumable;
    
    
    protected Item(string name, int goldValue)
    {
        _goldValue = goldValue;
        _name = name;
    }

    protected Item(string name)
    {
        _goldValue = 0;
        _name = name;
    }

    public abstract void Examine();
}