namespace ConsoleAdventure.Items;

public abstract class Armor : Item, IEquippable
{
    public int Protection { get; }

    protected Armor(bool price, string[] interactions, string name, int protection) : base(true, false, price, interactions, name)
    {
        Protection = protection;
    }
}