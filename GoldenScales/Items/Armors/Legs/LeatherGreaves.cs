//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class LeatherGreaves : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(1, 3);
    
    public LeatherGreaves() : base("Leather Greaves", ProtectionRange, EquipSlot.Legs)
    {
    }

    public override string StatsShort => "A piece of leather armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of leather armor.");
    }
}