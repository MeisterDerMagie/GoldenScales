//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Valuables;

public class SpellBook : Valuable
{
    public override string StatsShort => $"A valuable item. Value: {GoldValue}";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue}";
    
    public SpellBook(int goldValue) : base("Spellbook", goldValue + 5)
    {
        
    }


    public override void Examine()
    {
        Console.WriteLine("A spellbook filled with mystical writing. Unfortunately, you can't decipher any of it, but for a merchant this item should be of interest!");
    }
}