//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Weapons;

public class Axe : Weapon
{
    public override string StatsShort => "An axe. You can chop wood with it. Or skeletons.";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(3, 7);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(8, 12);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.05f, 0.10f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 3f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(3f, 4f);
    
    public Axe() : base("Axe", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public Axe(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Axe", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("An axe. You can chop wood with it. Or skeletons.");
    }
}