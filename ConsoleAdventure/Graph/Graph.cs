//(c) copyright by Martin M. Klöckener
using System.Globalization;

namespace ConsoleAdventure;
public class Graph
{
    public List<Vertex> vertices;
    public List<Edge> edges;

    //Constructor
    public Graph()
    {
        vertices = new List<Vertex>();
        edges = new List<Edge>();
    }
    
    public void AddVertex(Vertex _vertex)
    {
        //add new vertex to the graph
        vertices.Add(_vertex);
    }

    public void AddEdge(Edge _edge)
    {
        //add edge to the graph
        edges.Add(_edge);
        
        //let the source vertex know of the edge
        _edge.source.RegisterEdge(_edge);
        
        //let the target vertex know of the edge
        _edge.target.RegisterEdge(_edge);
    }

    public void RemoveVertex(Vertex _vertex)
    {
        //if the vertex is not part of the graph...
        if (!vertices.Contains(_vertex))
        {
            //let the user know and don't do anything
            Console.WriteLine($"Can't remove vertex \"{_vertex.ToString()}\" because it's not part of this graph!");
            return;
        }

        //iterate backwards over all edges of the graph...
        for (int i = edges.Count - 1; i >= 0; i--)
        {
            //... and if the edge is linked to the vertex we want to delete...
            if(edges[i].IncludesVertex(_vertex))
                //... also delete the edge.
                RemoveEdge(edges[i]);
        }

        //finally remove the vertex from the graph
        vertices.Remove(_vertex);
    }

    public void RemoveEdge(Edge _edge)
    {
        //let the source vertex know that this edge will be removed
        _edge.source.UnregisterEdge(_edge);
        
        //let the target vertex know that this edge will be removed
        _edge.target.UnregisterEdge(_edge);

        //remove the edge from the graph
        edges.Remove(_edge);
    }

    public override string ToString()
    {
        string output = string.Empty;

        //format vertices - format: V = {a, b, c}
        output += "V = {";

        for (int i = 0; i < vertices.Count; i++)
        {
            output += vertices[i].ToString();
            if (i < vertices.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //format edges - format: E = {(a, b), {a, c}, (c, b)}   (round braces for directed, curly braces for non-directed edges)
        output += "E = {";

        for (int i = 0; i < edges.Count; i++)
        {
            output += edges[i].ToString();
            if (i < edges.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //format weights - format: W = {{(a, b), 0.5}, {{a, c}, 0.33}}
        output += "W = {";

        for (int i = 0; i < edges.Count; i++)
        {
            output += "{";
            output += edges[i].ToString();
            output += ", ";
            output += Math.Round(edges[i].weight, 1).ToString(CultureInfo.InvariantCulture);
            output += "}";
            
            if (i < edges.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //return result
        return output;
    }
}