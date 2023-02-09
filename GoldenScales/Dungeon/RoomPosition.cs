//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public struct RoomPosition : IEquatable<RoomPosition>
{
    public uint X { get; }
    public uint Y { get; }

    public RoomPosition(uint x, uint y)
    {
        X = x;
        Y = y;
    }
    
    //Directions
    public RoomPosition North => new RoomPosition(X, Y - 1);
    public RoomPosition East => new RoomPosition(X + 1, Y);
    public RoomPosition South => new RoomPosition(X, Y + 1);
    public RoomPosition West => new RoomPosition(X - 1, Y);

    //Operator overloads
    public static bool operator ==(RoomPosition roomPosition1, RoomPosition roomPosition2) 
    {
        return roomPosition1.Equals(roomPosition2);
    }

    public static bool operator !=(RoomPosition roomPosition1, RoomPosition roomPosition2) 
    {
        return !roomPosition1.Equals(roomPosition2);
    }

    //IEquatable
    public bool Equals(RoomPosition other) => X == other.X && Y == other.Y;
    public override bool Equals(object obj) => obj is RoomPosition other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y);
    
    //ToString
    public override string ToString()
    {
        return $"({X.ToString()}, {Y.ToString()})";
    }
}