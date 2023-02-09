//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;
using ConsoleAdventure.Items.EnemyWeapons;

namespace ConsoleAdventure.Enemies;

public class Spider : Enemy
{
    public Spider() : base("Spider", Constants.SpiderMaxHealth, new SpiderBite()) { }
    
    protected override void Die()
    {
        Console.WriteLine("The spider wriggles its eight legs one last time, then breathes its last.");
        
        //generate loot and add to inventory
        var rng = new Random();
        Item lootOne = ItemFactory.GenerateRandomLootItem(rng.Next(Constants.EnemyLootGoldValueRange.Minimum, Constants.EnemyLootGoldValueRange.Maximum));
        Item lootTwo = ItemFactory.GenerateRandomLootItem(rng.Next(Constants.EnemyLootGoldValueRange.Minimum, Constants.EnemyLootGoldValueRange.Maximum));
            
        Player.Singleton.AddToIventory(lootOne, true);
        Player.Singleton.AddToIventory(lootTwo, true);

        Console.WriteLine($"You search the dead body and find a {lootOne.Name} and a {lootTwo.Name}.");
        
        base.Die();
    }
}