//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class ScaleGreaves : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(7, 10);
    
    public ScaleGreaves() : base("Scale Greaves", ProtectionRange, EquipSlot.Legs)
    {
    }

    public override string StatsShort => "A piece of scale armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of scale armor.");
    }
}