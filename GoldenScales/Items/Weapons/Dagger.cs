//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Weapons;

public class Dagger : Weapon
{
    public override string StatsShort => "A dagger. Better than a knife, worse than a sword. But very fast.";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(8, 12);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(13, 18);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.10f, 0.20f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(2f, 3.5f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(1f, 2f);
    
    public Dagger() : base("Dagger", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public Dagger(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Dagger", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("A dagger. Better than a knife, worse than a sword. But very fast.");
    }
}