//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Edge
{
    public bool IsDirected;
    public Vertex Source, Target;
    public float Weight;

    public Edge(Vertex source, Vertex target, bool isDirected = false)
    {
        IsDirected = isDirected;
        Source = source;
        Target = target;
        UpdateWeight();
    }
    
    public bool IncludesVertex(Vertex vertex) => (Source == vertex || Target == vertex);
    public bool IsLoop() => (Source == Target);
    
    public void UpdateWeight() => Weight = Vector2.Distance(Source.Position, Target.Position);

    public override string ToString()
    {
        string output = IsDirected ? $"({Source.ToString()}, {Target.ToString()})" : $"{{{Source.ToString()}, {Target.ToString()}}}";
        return output;
    }
}