namespace ConsoleAdventure.Items;

public abstract class Weapon : Equippable
{
    public int Damage { get; }

    protected Weapon(string name, int goldValue, int damage) : base(name, goldValue, EquipSlot.Weapon)
    {
        Damage = damage;
    }
}