//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Valuables;

public class SilverRing : Valuable
{
    public override string StatsShort => $"A valuable item.";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue}";
    
    public SilverRing(int goldValue) : base("Silver Ring", goldValue + 5)
    {
        
    }


    public override void Examine()
    {
        Console.WriteLine("A small silver ring. A merchant might be interested in it.");
    }
}