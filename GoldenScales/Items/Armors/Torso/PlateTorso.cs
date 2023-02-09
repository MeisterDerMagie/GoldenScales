//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class PlateTorso : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(11, 14);
    
    public PlateTorso() : base("Plate Armor", ProtectionRange, EquipSlot.Torso)
    {
    }

    public override string StatsShort => "A piece of plate armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of plate armor.");
    }
}