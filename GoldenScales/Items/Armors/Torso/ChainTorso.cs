//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class ChainTorso : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(5, 8);
    
    public ChainTorso() : base("Chain Armor", ProtectionRange, EquipSlot.Torso)
    {
    }

    public override string StatsShort => "A piece of chain armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of chain armor.");
    }
}