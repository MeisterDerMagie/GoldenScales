//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Consumables;

public class TinyHealthPotion : HealthPotion
{
    public override string StatsShort => $"a tiny potions that heals your wounds for {HealAmount} hp";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue} \nConsumable";
    
    public TinyHealthPotion() : base("Tiny Health Potion", Constants.ChestValueRange.Minimum)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("This is a tiny red potion that heals some of your wounds when you drink it. It's barely worth carrying it around, but better than nothing...");
    }

    protected override int HealAmount => 5;
}