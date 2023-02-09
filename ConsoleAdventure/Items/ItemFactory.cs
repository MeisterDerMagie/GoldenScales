//(c) copyright by Martin M. Klöckener
using System.Diagnostics.Contracts;
using ConsoleAdventure.Items.Consumables;

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
            //random weapon(s)
            () => Weapon.GenerateRandomWeapon(),
            //random armor(s)
            () => Armor.GenerateRandomArmor(),
            () => Armor.GenerateRandomArmor()
        };

        return GenerateRandomItem(possibleItems);
    }

    [Pure]
    public static Item GenerateRandomTraderItem(int goldValue)
    {
        Func<Item>[] possibleItems =
        {
            () => HealthPotion.CreateHealthPotionFromGoldValue(goldValue),
            //random weapon(s)
            () => Weapon.GenerateRandomWeapon(),
            //random armor(s)
            () => Armor.GenerateRandomArmor(),
            () => Armor.GenerateRandomArmor()
        };

        return GenerateRandomItem(possibleItems);
    }

    private static Item GenerateRandomItem(Func<Item>[] possibleItems)
    {
        int randomIndex = rng.Next(possibleItems.Length);
        return possibleItems[randomIndex]();
    }
}