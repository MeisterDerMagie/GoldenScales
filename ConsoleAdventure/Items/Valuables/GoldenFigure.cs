//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Valuables;

public class GoldenFigure : Item
{
    public override string StatsShort => $"A valuable item. Value: {GoldValue}";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue}";
    
    public GoldenFigure(int goldValue) : base("Golden Figure", goldValue)
    {
        
    }


    public override void Examine()
    {
        Console.WriteLine("It's a small statue made out of pure gold. It seems to be very valuable but has no other use. You should search for someone who is interested in it.");
    }
}