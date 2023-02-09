//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.EnemyWeapons;

public class MimicBite : EnemyWeapon
{
    public override string StatsShort => "MimicBite";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(3, 4);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(6, 9);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.05f, 0.10f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 2.5f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(1f, 2f);
    
    public MimicBite() : base("Mimic Bite", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public MimicBite(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Mimic Bite", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("MimicBite");
    }
}