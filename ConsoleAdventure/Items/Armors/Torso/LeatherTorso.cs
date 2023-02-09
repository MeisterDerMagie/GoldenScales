//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class LeatherTorso : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(2, 5);
    
    public LeatherTorso() : base("Leather Armor", ProtectionRange, EquipSlot.Torso)
    {
    }

    public override string StatsShort => "A piece of leather armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of leather armor.");
    }
}