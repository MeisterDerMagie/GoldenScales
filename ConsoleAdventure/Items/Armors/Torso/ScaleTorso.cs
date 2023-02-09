//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class ScaleTorso : Armor
{
    private static readonly Range<int> ProtectionRange = new Range<int>(8, 11);
    
    public ScaleTorso() : base("Scale Armor", ProtectionRange, EquipSlot.Torso)
    {
    }

    public override string StatsShort => "A piece of scale armor.";
    public override void Examine()
    {
        Console.WriteLine("A piece of scale armor.");
    }
}