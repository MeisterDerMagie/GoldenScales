using System.Text;
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items;

public abstract class Weapon : Equippable
{
    public override string StatsFull => GetFullStats();
    public Range<int> BaseDamage { get; }
    public float CritChance { get; }
    public float CritMultiplier { get; }
    public float AttackDuration { get; }
    public float Dps => CalculateDps();

    protected Weapon(string name, int goldValue, Range<int> baseDamage, float critChance, float critMultiplier, float attackDuration) : base(name, goldValue, EquipSlot.Weapon)
    {
        BaseDamage = baseDamage;
        CritChance = critChance;
        CritMultiplier = critMultiplier;
        AttackDuration = attackDuration;
    }

    //https://www.reddit.com/r/gamedesign/comments/51ue72/how_do_i_calculate_dps_and_take_into_account_crit/
    private float CalculateDps()
    {
        float baseDamagePerAttack = (BaseDamage.Minimum + BaseDamage.Maximum) / 2f;
        float critDamagePerAttack = baseDamagePerAttack * CritChance * CritMultiplier;
        float normalDamagePerAttack = baseDamagePerAttack * (1f - CritChance);
        float fullDamagePerAttack = critDamagePerAttack + normalDamagePerAttack;
        float damagePerSecond = fullDamagePerAttack / AttackDuration;

        return damagePerSecond;
    }

    private string GetFullStats()
    {
        var fullStats = new StringBuilder();

        fullStats.AppendLine($"Name: {Name}");
        fullStats.AppendLine($"Description: {StatsShort}");
        fullStats.AppendLine($"Base Damage: {BaseDamage.Minimum}-{BaseDamage.Maximum}");
        fullStats.AppendLine($"Crit Chance: {CritChance*100}%");
        fullStats.AppendLine($"Crit Multiplier: x{CritMultiplier}");
        fullStats.AppendLine($"Attack Duration: {AttackDuration} seconds");
        fullStats.AppendLine($"Value: {GoldValue}");
        fullStats.AppendLine($"Equipped: {(IsEquipped ? "yes" : "no")}");

        return fullStats.ToString();
    }
}