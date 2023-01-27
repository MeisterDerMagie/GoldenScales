//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Utilities;

public static class StringParser
{
    public static Direction DirectionFromString(string userParameter)
    {
        string lower = userParameter.ToLower();
        return lower switch
        {
            "north" => Direction.North,
            "up" => Direction.North,
            "east" => Direction.East,
            "right" => Direction.East,
            "south" => Direction.South,
            "down" => Direction.South,
            "west" => Direction.West,
            "left" => Direction.West,
            _ => Direction.NONE
        };
    }
}