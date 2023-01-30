//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Consumables;

public class MediumHealthPotion : HealthPotion
{
    public override string StatsShort => $"a medium potions that heals your wounds for {HealAmount} hp";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue} \nConsumable";
    
    public MediumHealthPotion() : base("Medium Health Potion", 10)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("This is a medium sized potion that heals some of your wounds when you drink it. It glows magically in the dark.");
    }

    protected override int HealAmount => 20;
}