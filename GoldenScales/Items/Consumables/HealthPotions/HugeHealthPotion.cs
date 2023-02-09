//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Consumables;

public class HugeHealthPotion: HealthPotion
{
    public override string StatsShort => $"a huge potions that heals your wounds for {HealAmount} hp. Very rare!";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue} \nConsumable";
    
    public HugeHealthPotion() : base("Huge Health Potion", Constants.ChestValueRange.Maximum)
    {
    }
    
    public override void Examine()
    {
        Console.WriteLine("This is a huge potion that heals a large part of your wounds when you drink it! You can feel the magical power surging from it. This item must be very rare!");
    }

    protected override int HealAmount => 50;
}