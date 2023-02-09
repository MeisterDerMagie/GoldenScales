//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.EnemyWeapons;

public class WizardSpell : EnemyWeapon
{
    public override string StatsShort => "WizardSpell";
    private static readonly Range<int> BaseDamageMinRange = new Range<int>(6, 10);
    private static readonly Range<int> BaseDamageMaxRange = new Range<int>(12, 18);
    private static readonly Range<float> CritChanceRange = new Range<float>(0.10f, 0.15f);
    private static readonly Range<float> CritMultiplierRange = new Range<float>(1.5f, 2.5f);
    private static readonly Range<float> AttackDurationRange = new Range<float>(4f, 8f);
    
    public WizardSpell() : base("Wizard Spell", BaseDamageMinRange, BaseDamageMaxRange, CritChanceRange, CritMultiplierRange, AttackDurationRange)
    {
    }
    
    public WizardSpell(Range<int> baseDamageMinRange, Range<int> baseDamageMaxRange, Range<float> critChanceRange, Range<float> critMultiplierRange, Range<float> attackDurationRange) : base("Wizard Spell", baseDamageMinRange, baseDamageMaxRange, critChanceRange, critMultiplierRange, attackDurationRange)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("WizardSpell");
    }
}