//(c) copyright by Martin M. Klöckener
using System.Diagnostics.Contracts;
using ConsoleAdventure.Items;
using ConsoleAdventure.Items.Consumables;
using ConsoleAdventure.Items.Valuables;

namespace ConsoleAdventure.Utilities;

public class ItemUtilities
{
    private static Random rng = new Random();
    
    //https://stackoverflow.com/questions/45267690/creating-an-object-of-a-random-sub-class-in-c-sharp
    [Pure]
    public static Item GenerateRandomItem(int goldValue)
    {
        Func<Item>[] possibleItems =
        {
            () => new GoldenFigure(goldValue),
            () => HealthPotion.CreateHealthPotionFromGoldValue(goldValue),
        };

        int randomIndex = rng.Next(possibleItems.Length);

        return possibleItems[randomIndex]();
    }
}