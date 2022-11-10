//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Door
{
    public bool isDirected;
    public Room source, target;
    public float weight;

    public Door(Room _source, Room _target, bool _isDirected = false)
    {
        isDirected = _isDirected;
        source = _source;
        target = _target;
        UpdateWeight();
    }
    
    public bool IncludesRoom(Room _room) => (source == _room || target == _room);
    public bool IsLoop() => (source == target);
    
    public void UpdateWeight() => weight = Vector2.Distance(source.position, target.position);

    public override string ToString()
    {
        string output = isDirected ? $"({source.ToString()}, {target.ToString()})" : $"{{{source.ToString()}, {target.ToString()}}}";
        return output;
    }
}