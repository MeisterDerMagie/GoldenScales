//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class ChainBoots : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(4, 7);
    
    public ChainBoots() : base("Chain Boots", ProtectionRange, EquipSlot.Feet)
    {
    }

    public override string StatsShort => "A piece of chain armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of chain armor.");
    }
}