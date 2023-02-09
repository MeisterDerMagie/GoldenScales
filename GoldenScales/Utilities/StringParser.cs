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

    public static (Item item, int itemNumber) InventoryItemFromString(List<string> userParameters, List<Item> inventory)
    {
        //if the inventory is empty, we can't find an item in there
        if (inventory.Count == 0)
        {
            Console.WriteLine("The inventory is empty.");
            return (null, -1);
        }
        
        foreach (string parameter in userParameters)
        {
            if (!int.TryParse(parameter, out int itemNumber)) continue;

            //if an int was found in the parameters, use it as the item number
            //convert the item number to a list index
            int itemIndex = itemNumber - 1;
            
            //check if it's a valid index
            if (itemIndex < 0 || itemIndex >= inventory.Count)
            {
                Console.WriteLine($"Item number {itemNumber} is not valid. Choose a number between 1 and {inventory.Count}.");
                break;
            }
            
            //if it's a valid index, return the item
            return (inventory[itemIndex], itemNumber);
        }
        
        //if no matching item was found, return null
        return (null, -1);
    }
}