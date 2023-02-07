//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;

namespace ConsoleAdventure.Items.Weapons;

public class Knife : Weapon
{
    public override string StatsShort => "A small knife. Not very effective but better than nothing.";

    public Knife(int goldValue, Range<int> baseDamage, float critChance, float critMultiplier, float attackDuration) : base("Knife", goldValue, baseDamage, critChance, attackDuration, critMultiplier)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("A small knife with a slightly rusty blade. This will not be good enough to fight the Lindworm, but for one or two skeletons it should be enough.");
    }
}