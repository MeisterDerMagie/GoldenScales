//(c) copyright by Martin M. Klöckener
using System.Globalization;

namespace ConsoleAdventure;
public class Graph
{
    public List<Vertex> Vertices;
    public List<Edge> Edges;

    //Constructor
    public Graph()
    {
        Vertices = new List<Vertex>();
        Edges = new List<Edge>();
    }
    
    public void AddVertex(Vertex vertex)
    {
        //add new vertex to the graph
        Vertices.Add(vertex);
    }

    public void AddEdge(Edge edge)
    {
        //add edge to the graph
        Edges.Add(edge);
        
        //let the source vertex know of the edge
        edge.Source.RegisterEdge(edge);
        
        //let the target vertex know of the edge
        edge.Target.RegisterEdge(edge);
    }

    public void RemoveVertex(Vertex vertex)
    {
        //if the vertex is not part of the graph...
        if (!Vertices.Contains(vertex))
        {
            //let the user know and don't do anything
            Console.WriteLine($"Can't remove vertex \"{vertex.ToString()}\" because it's not part of this graph!");
            return;
        }

        //iterate backwards over all edges of the graph...
        for (int i = Edges.Count - 1; i >= 0; i--)
        {
            //... and if the edge is linked to the vertex we want to delete...
            if(Edges[i].IncludesVertex(vertex))
                //... also delete the edge.
                RemoveEdge(Edges[i]);
        }

        //finally remove the vertex from the graph
        Vertices.Remove(vertex);
    }

    public void RemoveEdge(Edge edge)
    {
        //let the source vertex know that this edge will be removed
        edge.Source.UnregisterEdge(edge);
        
        //let the target vertex know that this edge will be removed
        edge.Target.UnregisterEdge(edge);

        //remove the edge from the graph
        Edges.Remove(edge);
    }

    public override string ToString()
    {
        string output = string.Empty;

        //format vertices - format: V = {a, b, c}
        output += "V = {";

        for (int i = 0; i < Vertices.Count; i++)
        {
            output += Vertices[i].ToString();
            if (i < Vertices.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //format edges - format: E = {(a, b), {a, c}, (c, b)}   (round braces for directed, curly braces for non-directed edges)
        output += "E = {";

        for (int i = 0; i < Edges.Count; i++)
        {
            output += Edges[i].ToString();
            if (i < Edges.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //format weights - format: W = {{(a, b), 0.5}, {{a, c}, 0.33}}
        output += "W = {";

        for (int i = 0; i < Edges.Count; i++)
        {
            output += "{";
            output += Edges[i].ToString();
            output += ", ";
            output += Math.Round(Edges[i].Weight, 1).ToString(CultureInfo.InvariantCulture);
            output += "}";
            
            if (i < Edges.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //return result
        return output;
    }
}