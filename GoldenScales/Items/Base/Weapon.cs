using System.Diagnostics.Contracts;
using System.Text;
using ConsoleAdventure.DataTypes;
using ConsoleAdventure.Items.Weapons;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure.Items;

public abstract class Weapon : Equippable
{
    public override string Name => (Quality == 0 || this is Fists) ? _name : $"{WeaponQuality.ToString()} {_name}";
    public override string StatsFull => GetFullStats();
    public Range<int> BaseDamage { get; }
    private readonly Range<int> _baseDamageMinRange;
    private readonly Range<int> _baseDamageMaxRange;
    public float CritChance { get; }
    private readonly Range<float> _critChanceRange;
    public float CritMultiplier { get; }
    private readonly Range<float> _critMultiplierRange;
    public float AttackDuration { get; }
    private readonly Range<float> _attackDurationRange;
    public float Dps => CalculateDps();
    public WeaponQuality WeaponQuality => (WeaponQuality)Quality;

    protected Weapon(string name, Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base(name, EquipSlot.Weapon)
    {
        _baseDamageMinRange = baseDamageMinRange;
        _baseDamageMaxRange = baseDamageMaxRange;
        _critChanceRange = critChanceRange;
        _critMultiplierRange = critMultiplierRange;
        _attackDurationRange = attackDurationRange;
        
        var rng = new Random();

        //-1: tendancy towards worse stats, 0: balanced, 1: tendancy towards better stats
        int tendancy = rng.Next(-1, 2);
        int drawings = 3;
        
        //generate base damage
        float rngResult = GetRandomNumberWithTendancy(tendancy, drawings);
        int baseDamageMin = (int)MathUtilities.Remap(rngResult, 0f, 1f, _baseDamageMinRange.Minimum, _baseDamageMinRange.Maximum + 1);
        
        rngResult = GetRandomNumberWithTendancy(tendancy, drawings);
        int baseDamageMax = (int)MathUtilities.Remap(rngResult, 0f, 1f, _baseDamageMaxRange.Minimum, _baseDamageMaxRange.Maximum + 1);
        
        BaseDamage = new Range<int>(baseDamageMin, baseDamageMax);

        //generate crit chance
        rngResult = GetRandomNumberWithTendancy(tendancy, drawings);
        float critChanceUnrounded = MathUtilities.Remap(rngResult, 0f, 1f, _critChanceRange.Minimum, _critChanceRange.Maximum);
        CritChance = MathF.Round(critChanceUnrounded, 2);

        //generate crit multiplier
        rngResult = GetRandomNumberWithTendancy(tendancy, drawings);
        float critMultiplierUnrounded = MathUtilities.Remap(rngResult, 0f, 1f, _critMultiplierRange.Minimum, _critMultiplierRange.Maximum);
        CritMultiplier = MathF.Round(critMultiplierUnrounded, 1);

        //generate attack duration
        rngResult = GetRandomNumberWithTendancy(tendancy, drawings, true);
        float attackDurationUnrounded = MathUtilities.Remap(rngResult, 0f, 1f, _attackDurationRange.Minimum, _attackDurationRange.Maximum);
        AttackDuration = MathF.Round(attackDurationUnrounded, 1);
    }

    public virtual int GetDamage()
    {
        var rng = new Random();

        int baseDamage = rng.Next(BaseDamage.Minimum, BaseDamage.Maximum + 1);
        bool isCritical = rng.NextSingle() < CritChance;
        int critDamage = isCritical ? (int)MathF.Round(baseDamage * CritMultiplier) : baseDamage;
        return critDamage;
    }

    private float GetRandomNumberWithTendancy(int tendancy, int drawings, bool invert = false)
    {
        if(tendancy == -1) return (invert) ? RandomUtilities.GetHighestValueOfDrawings(drawings) : RandomUtilities.GetLowestValueOfDrawings(drawings);
        if (tendancy == 0) return new Random().NextSingle();
        if (tendancy == 1) return (invert) ? RandomUtilities.GetLowestValueOfDrawings(drawings) : RandomUtilities.GetHighestValueOfDrawings(drawings);

        else
        {
            Console.WriteLine("ERROR: tendancy can only be -1, 0 or 1.");
            return new Random().NextSingle();
        }
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

    protected override int CalculateGoldValue()
    {
        return (int)MathF.Ceiling(Dps * Constants.WeaponGoldPerDps);
    }
    
    protected override int CalculateQuality()
    {
        //from -3 (bad) to 3 (legendary). 0 is normal quality
        
        float baseDamageMinPercent =  (_baseDamageMinRange.Minimum == _baseDamageMinRange.Maximum)   ? 0.5f : 1f / (_baseDamageMinRange.Maximum - _baseDamageMinRange.Minimum) * (BaseDamage.Minimum - _baseDamageMinRange.Minimum);
        float baseDamageMaxPercent =  (_baseDamageMaxRange.Minimum == _baseDamageMaxRange.Maximum)   ? 0.5f : 1f / (_baseDamageMaxRange.Maximum - _baseDamageMaxRange.Minimum) * (BaseDamage.Maximum - _baseDamageMaxRange.Minimum);
        float critChancePercent =     (_critChanceRange.Minimum == _critChanceRange.Maximum)         ? 0.5f : 1f / (_critChanceRange.Maximum - _critChanceRange.Minimum) * (CritChance - _critChanceRange.Minimum);
        float critMultiplierPercent = (_critMultiplierRange.Minimum == _critMultiplierRange.Maximum) ? 0.5f : 1f / (_critMultiplierRange.Maximum - _critMultiplierRange.Minimum) * (CritMultiplier - _critMultiplierRange.Minimum);
        float attackDurationPercent = (_attackDurationRange.Minimum == _attackDurationRange.Maximum) ? 0.5f : 1f - 1f / (_attackDurationRange.Maximum - _attackDurationRange.Minimum) * (AttackDuration - _attackDurationRange.Minimum);

        float totalPercent = (baseDamageMinPercent + baseDamageMaxPercent + critChancePercent + critMultiplierPercent + attackDurationPercent) / 5f;
        float quality = MathUtilities.Remap(totalPercent, 0f, 1f, -3.45f, 3.45f);
        
        return (int)MathF.Round(quality);
    }

    [Pure]
    public static Weapon GenerateRandomWeapon()
    {
        Func<Weapon>[] possibleWeapons =
        {
            () => new Axe(),
            () => new Dagger(),
            () => new Flail(),
            () => new Rapier(),
            () => new Spear(),
            () => new Sword()
        };

        var rng = new Random();
        int randomIndex = rng.Next(possibleWeapons.Length);
        return possibleWeapons[randomIndex]();
    }
}