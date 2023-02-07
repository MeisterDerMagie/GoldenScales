using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items;

public abstract class Weapon : Equippable
{
    public Range<int> BaseDamage { get; }
    public float CritChance { get; }
    public float CritMultiplier { get; }
    public float AttackDuration { get; }
    public float Dps => CalculateDps();

    protected Weapon(string name, int goldValue, Range<int> baseDamage, float critChance, float attackDuration, float critMultiplier) : base(name, goldValue, EquipSlot.Weapon)
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
}