//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class ScaleGloves : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(7, 10);
    
    public ScaleGloves() : base("Scale Gloves", ProtectionRange, EquipSlot.Hands)
    {
    }

    public override string StatsShort => "A piece of scale armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of scale armor.");
    }
}