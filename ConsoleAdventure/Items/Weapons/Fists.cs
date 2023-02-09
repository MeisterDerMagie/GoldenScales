//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Weapons;

public class Fists : Weapon
{
    public override string StatsShort => "Your fists. Try not to break them when attacking.";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(1, 2);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(3, 3);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.01f, 0.03f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.2f, 1.5f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(2f, 3f);
    
    public Fists() : base("Fists", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public Fists(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Fists", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("Your fists. Try not to break them when attacking.");
    }
}