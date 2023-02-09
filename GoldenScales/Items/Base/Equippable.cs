//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items;

public abstract class Equippable : Item
{
    public EquipSlot EquipSlot { get; }
    public bool IsEquipped => CheckIfEquipped();
    public int Quality => CalculateQuality();
    public override int GoldValue => CalculateGoldValue();

    protected Equippable(string name, EquipSlot slot) : base(name)
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

    protected abstract int CalculateGoldValue();
    protected abstract int CalculateQuality();
}