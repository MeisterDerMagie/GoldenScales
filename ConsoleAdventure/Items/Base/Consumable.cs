//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items;

public abstract class Consumable : Item
{
    protected Consumable(string name, int goldValue) : base(name, goldValue)
    {
    }

    public abstract void Consume();
}