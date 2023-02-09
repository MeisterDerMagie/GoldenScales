//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Weapons;

public class Sword : Weapon
{
    public override string StatsShort => "A sword. The knightly weapon par excellence.";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(12, 20);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(21, 28);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.05f, 0.2f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 3f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(3f, 4f);
    
    public Sword() : base("Sword", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public Sword(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Sword", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("A sword. The knightly weapon par excellence.");
    }
}