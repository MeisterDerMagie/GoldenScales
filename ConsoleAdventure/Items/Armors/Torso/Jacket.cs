//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Armors;

public class Jacket : Armor
{
    public Jacket(Range<int> protectionRange) : base("Jacket", protectionRange, EquipSlot.Torso)
    {
    }

    public override string StatsShort => "A simple jacket that keeps you warm but grants almost no protection from injuries.";
    public override void Examine()
    {
        Console.WriteLine("You've owned this jacket for a few years now. It's comfortable, but offers little protection in battle.");
    }
}