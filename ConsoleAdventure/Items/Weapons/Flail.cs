//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Weapons;

public class Flail : Weapon
{
    public override string StatsShort => "A somewhat unhandy weapon, but it does good damage.";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(15, 20);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(22, 26);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.05f, 0.10f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 2f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(4f, 6f);
    
    public Flail() : base("Flail", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public Flail(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Flail", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("A somewhat unhandy weapon, but it does good damage.");
    }
}