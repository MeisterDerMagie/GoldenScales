using System.Text;

namespace ConsoleAdventure.Items;

public abstract class Armor : Equippable
{
    public override string StatsFull => GetFullStats();

    public int Protection { get; }

    protected Armor(string name, int goldValue, int protection, EquipSlot equipSlot) : base(name, goldValue, equipSlot)
    {
        Protection = protection;
    }
    
    private string GetFullStats()
    {
        var fullStats = new StringBuilder();

        fullStats.AppendLine($"Name: {Name}");
        fullStats.AppendLine($"Description: {StatsShort}");
        fullStats.AppendLine($"Protection: {Protection}");
        fullStats.AppendLine($"Slot: {EquipSlot.ToString()}");
        fullStats.AppendLine($"Equipped: {(IsEquipped ? "yes" : "no")}");

        return fullStats.ToString();
    }
}