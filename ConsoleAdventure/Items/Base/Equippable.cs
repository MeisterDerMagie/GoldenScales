//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items;

public abstract class Equippable : Item
{
    public EquipSlot EquipSlot { get; }
    
    protected Equippable(string name, int goldValue, EquipSlot slot) : base(name, goldValue)
    {
        EquipSlot = slot;
    }

    public abstract void Equip();

    public abstract void UnEquip();
}