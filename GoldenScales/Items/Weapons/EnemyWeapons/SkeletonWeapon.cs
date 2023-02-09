//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.EnemyWeapons;

public class SkeletonWeapon : EnemyWeapon
{
    public override string StatsShort => "SkeletonWeapon";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(2, 4);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(5, 8);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.05f, 0.10f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 2f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(3f, 4f);
    
    public SkeletonWeapon() : base("Skeleton Weapon", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public SkeletonWeapon(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Skeleton Weapon", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("SkeletonWeapon");
    }
}