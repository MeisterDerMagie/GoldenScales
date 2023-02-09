//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Weapons;

public class Rapier : Weapon
{
    public override string StatsShort => "A rapier. Whoever knows how to wield this weapon wreaks havoc.";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(8, 18);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(19, 25);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.3f, 0.5f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(2f, 4f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(2f, 4f);
    
    public Rapier() : base("Rapier", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public Rapier(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Rapier", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("A rapier. Whoever knows how to wield this weapon wreaks havoc.");
    }
}