//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public struct RoomPosition
{
    public uint X { get; }
    public uint Y { get; }

    public RoomPosition(uint x, uint y)
    {
        X = x;
        Y = y;
    }
}