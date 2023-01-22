//(c) copyright by Martin M. Klöckener

using System.Diagnostics.Contracts;

namespace ConsoleAdventure.Utilities;

public static class RandomUtilities
{
    private static Random rng = new ();

    public static void SetSeed(int seed)
    {
        rng = new Random(seed);
    }
    
    public static bool RandomBool()
    {
        int zeroOrOne = rng.Next(0, 2);
        return zeroOrOne == 1;
    }

    public static bool RandomBoolWeighted(float probabilityOfSuccess)
    {
        float random = (float)rng.NextDouble();
        return random < probabilityOfSuccess;
    }

    [Pure]
    public static int RandomInt(int min, int max)
    {
        return rng.Next(min, max);
    }

    public static Room RandomRoom(Dungeon dungeon)
    {
        int roomCount = dungeon.Rooms.Count;
        int randomIndex = rng.Next(roomCount);

        return dungeon.Rooms[randomIndex];
    }
    
    //-- Random element with probability --
    public static T RandomElement<T>(List<ElementProbability<T>> elementsProbabilities)
    {
        //ensure that the total probability of all elements is 1.0
        float sum = 0f;
        foreach (ElementProbability<T> element in elementsProbabilities)
        {
            sum += element.Probability;
        }
        
        if (Math.Abs(sum - 1f) > 0.0001)
            throw new Exception($"The probability of all elements needs to add up to exactly 1.0! It was {sum}.");
        
        //random number between 0.0 and 1.0
        float random = (float)rng.NextDouble();

        //order elements
        elementsProbabilities = elementsProbabilities.OrderBy(element => element.Probability).ToList();
        
        //pick random element
        float cumulative = 0f;
        foreach (ElementProbability<T> element in elementsProbabilities)
        {
            cumulative += element.Probability;
            if (!(random <= cumulative)) continue;
            
            return element.Element;
        }
        
        //if we land here, something went wrong
        throw new Exception($"Could not pick a random element. Something went wrong. Random number was {random}");
    }

    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }
}

public class ElementProbability<T>
{
    public T Element { get; }
    public float Probability { get; }

    public ElementProbability(T element, float probability)
    {
        if (probability is < 0f or > 1f)
            throw new Exception("Probability needs to be between 0.0 and 1.0!");
        
        Element = element;
        Probability = probability;
    }
}