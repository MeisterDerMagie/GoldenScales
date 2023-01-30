//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Consumables;

public class SmallHealthPotion : HealthPotion
{
    public override string StatsShort => $"a small potions that heals your wounds for {HealAmount} hp";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue} \nConsumable";
    
    public SmallHealthPotion() : base("Small Health Potion", 5)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("This is a small red potion that heals some of your wounds when you drink it. It glows magically in the dark.");
    }

    protected override int HealAmount => 10;
}