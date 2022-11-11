namespace ConsoleAdventure.Items;

public abstract class Armor : Item
{
    public int Protection { get; }

    protected Armor(bool equippable, bool price, string[] interactions, string name, int protection) : base(equippable, price, interactions, name)
    {
        Protection = protection;
    }
}