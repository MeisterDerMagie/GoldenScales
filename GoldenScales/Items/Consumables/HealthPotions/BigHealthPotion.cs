//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Consumables;

public class BigHealthPotion : HealthPotion
{
    public override string StatsShort => $"a big potions that heals your wounds for {HealAmount} hp";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue} \nConsumable";
    
    public BigHealthPotion() : base("Big Health Potion", 15)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("This is a big potion that heals a large part of your wounds when you drink it. You can feel the magical power surging from it.");
    }

    protected override int HealAmount => 30;
}