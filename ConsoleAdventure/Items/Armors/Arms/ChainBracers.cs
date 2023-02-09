//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class ChainBracers : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(4, 7);
    
    public ChainBracers() : base("Chain Bracers", ProtectionRange, EquipSlot.Arms)
    {
    }

    public override string StatsShort => "A piece of chain armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of chain armor.");
    }
}