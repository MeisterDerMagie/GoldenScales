//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class LeatherHelmet : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(2, 4);
    
    public LeatherHelmet() : base("Leather Helmet", ProtectionRange, EquipSlot.Head)
    {
    }

    public override string StatsShort => "A piece of leather armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of leather armor.");
    }
}