//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class PlateHelmet : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(10, 14);
    
    public PlateHelmet() : base("Plate Helmet", ProtectionRange, EquipSlot.Head)
    {
    }

    public override string StatsShort => "A piece of plate armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of plate armor.");
    }
}