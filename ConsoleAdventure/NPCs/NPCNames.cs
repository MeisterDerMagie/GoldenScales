//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.NPCs;

public class NPCNames
{
    private static Random rng = new();
    
    public static string GetRandomNPCName()
    {
        int randomIndex = rng.Next(names.Count);
        return names[randomIndex];
    }

    private static List<string> names = new List<string>()
    {
        "Girard",
        "Bartelot",
        "Wichard",
        "Randulfus",
        "Gervase",
        "Ancelm",
        "Ihon",
        "Theodric",
        "Filbert",
        "Humphery",
        "Reynald",
        "Heudebrand",
        "Isemberd",
        "Malgerius",
        "Drewett",
        "Godebert",
        "Barnet",
        "Ragnfred",
        "Tobold",
        "Sigebert"
    };
}