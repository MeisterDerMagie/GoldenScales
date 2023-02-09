//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class ChainGloves : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(4, 7);
    
    public ChainGloves() : base("Chain Gloves", ProtectionRange, EquipSlot.Hands)
    {
    }

    public override string StatsShort => "A piece of chain armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of chain armor.");
    }
}