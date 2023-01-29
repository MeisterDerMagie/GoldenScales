//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Consumables;

public abstract class HealthPotion : Consumable
{
    public HealthPotion(string name, int goldValue) : base(name, goldValue)
    {
    }

    protected abstract int HealAmount { get; }
    
    public override void Consume()
    {
        Console.WriteLine($"You drink your {Name} and immediately feel new life rushing through your body. What a soothing feeling!");
        Player.Singleton.Heal(HealAmount);
    }

    public static HealthPotion CreateHealthPotionFromGoldValue(int goldValue)
    {
        if (goldValue <= Constants.ChestValueRange.Minimum)
            return new TinyHealthPotion();
        
        if (goldValue <= 7)
            return new SmallHealthPotion();
        
        if (goldValue <= 12)
            return new MediumHealthPotion();
        
        if (goldValue < Constants.ChestValueRange.Maximum)
            return new BigHealthPotion();
        
        if (goldValue >= Constants.ChestValueRange.Maximum)
            return new HugeHealthPotion();

        return new MediumHealthPotion();
    }
}