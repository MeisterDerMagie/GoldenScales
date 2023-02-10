//(c) copyright by Martin M. Klöckener
using System.Diagnostics.Contracts;
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
        
        Console.WriteLine($"\n{trader.Name} offers the following goods for sale:");
        
        PrintInventoryItems(trader.Inventory, showGoldValue, "purchase price:");
    }
    
    private static void PrintInventoryItems(List<Item> inventory, bool showGoldValue = false, string goldValuePrefix = "")
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            int itemNumber = i + 1;
            Item item = inventory[i];

            bool isEquipped = (item is Equippable equippable && equippable.IsEquipped);
            string equipped = isEquipped ? "[equipped] " : string.Empty;
            string goldValue = showGoldValue ? $"[{goldValuePrefix} {item.GoldValue}]" : string.Empty;
            string itemString = $"#{itemNumber}: {equipped}{item.Name} {goldValue} ({item.StatsShort})";
            Console.WriteLine(itemString);
        }
    }

    public static int CalculateTotalGoldValueOfAllInventoryItems(List<Item> inventory)
    {
        return inventory.Sum(item => item.GoldValue);
    }
    
    //sorts the inventory in this order (each category internally sorted by gold value): Consumables --> Equipped Weapon --> Equipped Armor --> non-equipped Weapons --> non-equipped Armor --> Valuables
    public static void SortInventory(List<Item> inventory)
    {
        var consumables = new List<Consumable>();
        Weapon equippedWeapon = null;
        var equippedArmor = new List<Armor>();
        var nonEquippedWeapons = new List<Weapon>();
        var nonEquippedArmor = new List<Armor>();
        var valuables = new List<Valuable>();
        var rest = new List<Item>(); //this should never have any items

        //sort into categories
        foreach (Item item in inventory)
        {
            if (item is Consumable consumable) consumables.Add(consumable);
            else if (item is Weapon weapon)
            {
                if (weapon.IsEquipped) equippedWeapon = weapon;
                else nonEquippedWeapons.Add(weapon);
            }
            else if (item is Armor armor)
            {
                if (armor.IsEquipped) equippedArmor.Add(armor);
                else nonEquippedArmor.Add(armor);
            }
            else if(item is Valuable valuable) valuables.Add(valuable);
            else
            {
                Console.WriteLine($"ERROR: Can't sort the item {item.Name} because it doesn't match any category.");
                rest.Add(item);
            }
        }
        
        //sort categories by gold value
        consumables = consumables.OrderBy(item => item.GoldValue).ToList();
        equippedArmor = equippedArmor.OrderByDescending(item => item.GoldValue).ToList();
        nonEquippedWeapons = nonEquippedWeapons.OrderByDescending(item => item.GoldValue).ToList();
        nonEquippedArmor = nonEquippedArmor.OrderByDescending(item => item.GoldValue).ToList();
        valuables = valuables.OrderByDescending(item => item.GoldValue).ToList();
        rest = rest.OrderByDescending(item => item.GoldValue).ToList();
        
        //clear inventory
        inventory.Clear();
        
        //re-add to inventory in order
        inventory.AddRange(consumables);
        if(equippedWeapon != null) inventory.Add(equippedWeapon);
        inventory.AddRange(equippedArmor);
        inventory.AddRange(nonEquippedWeapons);
        inventory.AddRange(nonEquippedArmor);
        inventory.AddRange(valuables);
        inventory.AddRange(rest);
    }
}