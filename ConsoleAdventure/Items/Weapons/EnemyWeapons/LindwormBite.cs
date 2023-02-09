//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.EnemyWeapons;

public class LindwormBite : EnemyWeapon
{
    public override string StatsShort => "LindwormBite";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(10, 15);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(20, 25);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.05f, 0.10f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 2f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(3f, 4f);
    
    public LindwormBite() : base("Lindworm Bite", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public LindwormBite(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Lindworm Bite", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("LindwormBite");
    }
}