//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.EnemyWeapons;

public class SpiderBite : EnemyWeapon
{
    public override string StatsShort => "SpiderBite";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(3, 4);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(6, 9);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.10f, 0.15f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 2.5f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(2f, 3f);
    
    public SpiderBite() : base("Spider Bite", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public SpiderBite(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Spider Bite", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("SpiderBite");
    }
}