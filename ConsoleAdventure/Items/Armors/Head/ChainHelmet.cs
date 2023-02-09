//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class ChainHelmet : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(4, 8);
    
    public ChainHelmet() : base("Chain Helmet", ProtectionRange, EquipSlot.Head)
    {
    }

    public override string StatsShort => "A piece of chain armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of chain armor.");
    }
}