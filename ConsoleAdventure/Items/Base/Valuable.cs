//(c) copyright by Martin M. Klöckener
using System.Diagnostics.Contracts;
using ConsoleAdventure.Items.Consumables;
using ConsoleAdventure.Items.Valuables;

namespace ConsoleAdventure.Items;

public abstract class Valuable : Item
{
    //valuables have no use other than being sold at a trader. That's why most of them get an additional goldValue on creation
    protected Valuable(string name, int goldValue) : base(name, goldValue)
    {
        
    }
    
    [Pure]
    public static Valuable GenerateRandomValuable(int goldValue)
    {
        Func<Valuable>[] possibleItems =
        {
            () => new GoldenFigure(goldValue),
            () => new Emerald(goldValue),
            () => new SilverRing(goldValue),
            () => new AncientCoin(goldValue),
            
        };

        var rng = new Random();
        int randomIndex = rng.Next(possibleItems.Length);

        return possibleItems[randomIndex]();
    }
}