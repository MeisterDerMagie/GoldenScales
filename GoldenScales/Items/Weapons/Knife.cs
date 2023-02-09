//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Weapons;

public class Knife : Weapon
{
    public override string StatsShort => "A small knife. Not very effective but better than nothing.";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(1, 4);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(5, 8);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.02f, 0.06f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 2.5f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(2f, 3f);
    
    public Knife() : base("Knife", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public Knife(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Knife", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("A small knife with a slightly rusty blade. This will not be good enough to fight the Lindworm, but for one or two skeletons it should be enough.");
    }
}