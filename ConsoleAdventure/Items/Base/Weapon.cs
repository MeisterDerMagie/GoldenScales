namespace ConsoleAdventure.Items;

public abstract class Weapon : Item
{
    public int Damage { get; }

    protected Weapon(bool equippable, bool price, string[] interactions, string name, int damage) : base(equippable, price, interactions, name)
    {
        Damage = damage;
    }
}