namespace ConsoleAdventure.Items;

public abstract class Weapon : Item, IEquippable
{
    public int Damage { get; }

    protected Weapon(bool equippable, bool consumable, bool price, string[] interactions, string name, int damage) : base(equippable, consumable, price, interactions, name)
    {
        Damage = damage;
    }
}