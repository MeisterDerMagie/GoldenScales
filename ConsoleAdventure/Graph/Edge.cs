//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Edge
{
    public bool isDirected;
    public Vertex source, target;
    public float weight;

    public Edge(Vertex _source, Vertex _target, bool _isDirected = false)
    {
        isDirected = _isDirected;
        source = _source;
        target = _target;
        UpdateWeight();
    }
    
    public bool IncludesVertex(Vertex _vertex) => (source == _vertex || target == _vertex);
    public bool IsLoop() => (source == target);
    
    public void UpdateWeight() => weight = Vector2.Distance(source.position, target.position);

    public override string ToString()
    {
        string output = isDirected ? $"({source.ToString()}, {target.ToString()})" : $"{{{source.ToString()}, {target.ToString()}}}";
        return output;
    }
}