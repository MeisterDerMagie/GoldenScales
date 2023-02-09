//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class PlateGreaves : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(10, 13);
    
    public PlateGreaves() : base("Plate Greaves", ProtectionRange, EquipSlot.Legs)
    {
    }

    public override string StatsShort => "A piece of plate armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of plate armor.");
    }
}