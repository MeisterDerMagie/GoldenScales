//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Valuables;

public class Emerald : Valuable
{
    public override string StatsShort => $"A valuable item.";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue}";
    
    public Emerald(int goldValue) : base("Emerald", goldValue + 7)
    {
        
    }


    public override void Examine()
    {
        Console.WriteLine("A precious gemstone that sparkles in the light. Beautiful!");
    }
}