//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Door
{
    public readonly Room Source, Target;
    public bool IsLocked;

    public Door(Room source, Room target, bool isLocked)
    {
        Source = source;
        Target = target;
        IsLocked = isLocked;
    }

    public bool IncludesRoom(Room room) => (Source == room || Target == room);
    public bool IsLoop() => (Source == Target);

    public override string ToString()
    {
        string output = $"{{{Source.ToString()}, {Target.ToString()}}}";
        return output;
    }
}