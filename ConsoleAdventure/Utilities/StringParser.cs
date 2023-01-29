//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Items;

namespace ConsoleAdventure.Utilities;

public static class StringParser
{
    public static Direction DirectionFromString(string userParameter)
    {
        string lower = userParameter.ToLower();
        return lower switch
        {
            "north" => Direction.North,
            "up" => Direction.North,
            "east" => Direction.East,
            "right" => Direction.East,
            "south" => Direction.South,
            "down" => Direction.South,
            "west" => Direction.West,
            "left" => Direction.West,
            _ => Direction.NONE
        };
    }

    public static Item InventoryItemFromString(List<string> userParameters)
    {
        //if the player as an empty inventory, we can't find an item in there
        if (Player.Singleton.Inventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty.");
            return null;
        }
        
        foreach (string parameter in userParameters)
        {
            if (!int.TryParse(parameter, out int itemIndex)) continue;

            //if an int was found in the parameters, use it as the item index
            //convert the itemIndex to a list index
            int itemListIndex = itemIndex -1;
            
            //check if it's a valid index
            if (itemListIndex < 0 || itemListIndex >= Player.Singleton.Inventory.Count)
            {
                Console.WriteLine($"Item number {itemIndex} is not valid. Choose a number between 1 and {Player.Singleton.Inventory.Count}.");
                break;
            }
            
            //if it's a valid index, return the item
            return Player.Singleton.Inventory[itemListIndex];
        }
        
        //if no matching item was found, return null
        return null;
    }
}