//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class ScaleHelmet : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(7, 11);
    
    public ScaleHelmet() : base("Scale Helmet", ProtectionRange, EquipSlot.Head)
    {
    }

    public override string StatsShort => "A piece of scale armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of scale armor.");
    }
}