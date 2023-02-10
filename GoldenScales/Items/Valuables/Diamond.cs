//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Valuables;

public class Diamond : Valuable
{
    public override string StatsShort => $"A valuable item.";
    public override string StatsFull => $"Name: {Name} \nDescription: {StatsShort} \nValue: {GoldValue}";
    
    public Diamond() : base("Diamond", Constants.ValueRareDiamondInEmptyRoom)
    {
    }


    public override void Examine()
    {
        Console.WriteLine("A diamond you found in the dirt. It sparkles enchantingly beautiful in the light of your torch. This must be a truly rare find!");
    }
}