//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class LeatherGloves : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(1, 3);
    
    public LeatherGloves() : base("Leather Gloves", ProtectionRange, EquipSlot.Hands)
    {
    }

    public override string StatsShort => "A piece of leather armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of leather armor.");
    }
}