namespace ConsoleAdventure.Items;

public abstract class Armor : Equippable
{
    public int Protection { get; }

    protected Armor(string name, int goldValue, int protection, EquipSlot equipSlot) : base(name, goldValue, equipSlot)
    {
        Protection = protection;
    }
}