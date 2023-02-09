//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;
using ConsoleAdventure.Items.EnemyWeapons;

namespace ConsoleAdventure.Enemies;

public class Skeleton : Enemy
{
    public Skeleton() : base("Skeleton", Constants.SkeletonMaxHealth, new SkeletonWeapon()) { }
    
    protected override void Die()
    {
        Console.WriteLine("The skeleton rattles one last time and collapses into a pile of bones.");
        
        //generate loot and add to inventory
        var rng = new Random();
        Item lootOne = ItemFactory.GenerateRandomLootItem(rng.Next(Constants.EnemyLootGoldValueRange.Minimum, Constants.EnemyLootGoldValueRange.Maximum));
        
        Player.Singleton.AddToIventory(lootOne, true);

        Console.WriteLine($"You search the dead body and find a {lootOne.Name}.");
        
        base.Die();
    }
}