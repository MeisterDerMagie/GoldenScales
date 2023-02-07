//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items;

public abstract class Equippable : Item
{
    public EquipSlot EquipSlot { get; }
    public bool IsEquipped => CheckIfEquipped();
    
    protected Equippable(string name, int goldValue, EquipSlot slot) : base(name, goldValue)
    {
        EquipSlot = slot;
    }

    private bool CheckIfEquipped()
    {
        foreach (KeyValuePair<EquipSlot, Equippable> equippedItem in Player.Singleton.EquippedItems)
        {
            if (equippedItem.Value == this) return true;
        }

        return false;
    }
}