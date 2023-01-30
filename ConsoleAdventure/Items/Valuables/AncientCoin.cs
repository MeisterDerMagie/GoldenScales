//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Valuables;

public class AncientCoin : Valuable
{
    public override string StatsShort => $"A valuable item. Value: {GoldValue}";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue}";
    
    public AncientCoin(int goldValue) : base("Ancient Coin", goldValue + 2)
    {
        
    }


    public override void Examine()
    {
        Console.WriteLine("An ancient coin. You can't pay with it, but since it's made of gold, it's still valuable.");
    }
}