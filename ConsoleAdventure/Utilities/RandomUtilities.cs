//(c) copyright by Martin M. Klöckener

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

    //https://jonlabelle.com/snippets/view/csharp/pick-random-elements-based-on-probability
    public static Direction RandomDirection(float probabilityNorth, float probabilityEast, float probabilitySouth, float probabilityWest)
    {
        float sum = probabilityNorth + probabilityEast + probabilitySouth + probabilityWest;
        if (Math.Abs(sum - 1f) > 0.0001)
            throw new Exception($"The probability of all directions needs to add up to exactly 1.0! It was {sum}.");
        
        float random = (float)rng.NextDouble();
        
        var probabilities = new List<(Direction direction, float probability)>() { (Direction.North, probabilityNorth), (Direction.East, probabilityEast), (Direction.South, probabilitySouth), (Direction.West, probabilityWest) };
        probabilities = probabilities.OrderBy(t => t.probability).ToList();

        float cumulative = 0f;
        
        for (int i = 0; i < probabilities.Count; i++)
        {
            cumulative += probabilities[i].probability;
            if (!(random <= cumulative)) continue;
            
            return probabilities[i].direction;
        }
        
        throw new Exception($"Could not pick a random direction. Something went wrong. Random number was {random}");
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