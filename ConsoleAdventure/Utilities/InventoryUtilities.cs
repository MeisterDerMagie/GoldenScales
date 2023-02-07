//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;
using ConsoleAdventure.NPCs;

namespace ConsoleAdventure.Utilities;

public static class InventoryUtilities
{
    public static void PrintPlayerInventory(Player player)
    {
        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty.");
            return;
        }
        
        Console.WriteLine("You have the following items in your inventory: ");
        
        PrintInventoryItems(player.Inventory);
    }
    
    public static void PrintPlayerInventoryTrading(Player player)
    {
        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty. There is nothing you can sell.");
            return;
        }
        
        Console.WriteLine("You can sell the following items from your inventory: ");
        
        PrintInventoryItems(player.Inventory, true, "selling price:");
    }

    public static void PrintTraderInventory(Trader trader, bool showGoldValue)
    {
        if (trader.Inventory.Count == 0)
        {
            Console.WriteLine($"{trader.Name} doesn't have any more items to sell.");
            return;
        }
        
        Console.WriteLine($"{trader.Name} offers the following goods for sale:");
        
        PrintInventoryItems(trader.Inventory, showGoldValue, "purchase price:");
    }
    
    private static void PrintInventoryItems(List<Item> inventory, bool showGoldValue = false, string goldValuePrefix = "")
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            int itemNumber = i + 1;
            Item item = inventory[i];

            string goldValue = showGoldValue ? $"[{goldValuePrefix} {item.GoldValue}]" : string.Empty;
            string itemString = $"#{itemNumber}: {item.Name} {goldValue} ({item.StatsShort})";
            Console.WriteLine(itemString);
        }
    }
}