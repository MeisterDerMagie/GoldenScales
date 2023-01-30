//(c) copyright by Martin M. Klöckener
using System.Diagnostics.Contracts;
using ConsoleAdventure.Items.Consumables;
using ConsoleAdventure.Items.Valuables;

namespace ConsoleAdventure.Items;

//https://stackoverflow.com/questions/45267690/creating-an-object-of-a-random-sub-class-in-c-sharp
public static class ItemFactory
{
    private static Random rng = new Random();
    
    [Pure]
    public static Item GenerateRandomLootItem(int goldValue)
    {
        Func<Item>[] possibleItems =
        {
            () => Valuable.GenerateRandomValuable(goldValue),
            () => HealthPotion.CreateHealthPotionFromGoldValue(goldValue),
            //random weapon
            //random armor
        };

        return GenerateRandomItem(goldValue, possibleItems);
    }

    [Pure]
    public static Item GenerateRandomTraderItem(int goldValue)
    {
        Func<Item>[] possibleItems =
        {
            () => HealthPotion.CreateHealthPotionFromGoldValue(goldValue),
            //random weapon
            //random armor
        };

        return GenerateRandomItem(goldValue, possibleItems);
    }

    private static Item GenerateRandomItem(int goldValue, Func<Item>[] possibleItems)
    {
        int randomIndex = rng.Next(possibleItems.Length);
        return possibleItems[randomIndex]();
    }
}