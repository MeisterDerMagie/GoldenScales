//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Weapons;

public class Spear : Weapon
{
    public override string StatsShort => "A long spear. A bit unwieldy, but if you hit, all the more effective.";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(14, 18);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(19, 24);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.05f, 0.15f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 3f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(6f, 8f);
    
    public Spear() : base("Spear", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public Spear(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Spear", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("A long spear. A bit unwieldy, but if you hit, all the more effective.");
    }
}