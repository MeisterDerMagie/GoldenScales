namespace ConsoleAdventure.Items;

public abstract class Weapon : Item, IEquippable
{
    public int Damage { get; }

    protected Weapon(bool equippable, bool consumable, int value, List<Command> interactions, string name, int damage) : base(equippable, consumable, value, interactions, name)
    {
        Damage = damage;
    }
}