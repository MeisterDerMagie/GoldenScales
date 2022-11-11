//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Door
{
    public bool IsDirected;
    public readonly Room Source, Target;
    //public float Weight;

    public Door(Room source, Room target, bool isDirected = false)
    {
        IsDirected = isDirected;
        Source = source;
        Target = target;
        //UpdateWeight();
    }
    
    public bool IncludesRoom(Room room) => (Source == room || Target == room);
    public bool IsLoop() => (Source == Target);
    
    //public void UpdateWeight() => Weight = Vector2.Distance(Source.Position, Target.Position);

    public override string ToString()
    {
        string output = IsDirected ? $"({Source.ToString()}, {Target.ToString()})" : $"{{{Source.ToString()}, {Target.ToString()}}}";
        return output;
    }
}