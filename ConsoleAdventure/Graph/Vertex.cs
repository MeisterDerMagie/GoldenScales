//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Vertex
{
    public string Name;
    public Vector2 Position;
    private HashSet<Edge> linkedEdges; //all linked edges, regardless of if they are directional or non-directional
    private HashSet<Vertex> linkedVertices; //all linked vertices, regardless of if they are connected with directional or non-directional edges
    
    /// <summary> All out-edges. Does not include "incoming" directed edges. </summary>
    public HashSet<Edge> OutEdges;
    
    /// <summary>All vertices that can be travelled to from this vertex. Takes directional edges into consideration. </summary>
    public HashSet<Vertex> OutVertices;

    //Constructor
    public Vertex(string name, Vector2 position)
    {
        Name = name;
        Position = position;
        linkedEdges = new HashSet<Edge>();
        linkedVertices = new HashSet<Vertex>();
        OutEdges = new HashSet<Edge>();
        OutVertices = new HashSet<Vertex>();
    }

    //if a new edge got added, we need to also reference it inside the linked vertices
    public void RegisterEdge(Edge edge)
    {
        if (!edge.IncludesVertex(this))
        {
            Console.WriteLine($"Can't register the edge {edge.ToString()} because it's not linked to this vertex {this.ToString()}!");
            return;
        }
        
        linkedEdges.Add(edge);
        Update();
    }

    //if an edge got delted, we need to also remove the reference from the lists in this vertex
    public void UnregisterEdge(Edge edge)
    {
        if (!edge.IncludesVertex(this))
        {
            Console.WriteLine($"Can't unregister the edge {edge.ToString()} because it's not linked to this vertex {this.ToString()}!");
            return;
        }

        linkedEdges.Remove(edge);
        
        Update();
    }

    public void SetPosition(Vector2 newPos)
    {
        //change the position of this vertex
        Position = newPos;

        //let all edges that are linked to this vertex know about the position change, so they can update the weight (distance) accordingly
        foreach (Edge edge in linkedEdges)
        {
            edge.UpdateWeight();
        }
    }
    
    //Update the linked vertices, out-edges and out-vertices
    private void Update()
    {
        //-- Refresh all linked vertices --
        //clean up
        linkedVertices.Clear();

        //iterate over all edges that are linked to this vertex...
        foreach (Edge edge in linkedEdges)
        {
            //if this vertex is the source of an edge ...
            if(edge.Source == this)
                //... add the target to the linked vertices
                linkedVertices.Add(edge.Target);
            
            //if this vertex is the target of an edge ...
            else if(edge.Target == this)
                //... add the source to the linked vertices
                linkedVertices.Add(edge.Source);
        }
        
        //-- Refresh all connected edges --
        //clean up
        OutEdges.Clear();
        
        //iterate over all edges that are linked to this vertex...
        foreach (Edge edge in linkedEdges)
        {
            //if this vertex is the target of an edge...
            if(edge.Target == this)
                //... and if the edge is directed ...
                if(edge.IsDirected)
                    //... don't add it to the connected edges list
                    continue;
            
            //otherwise add the edge
            OutEdges.Add(edge);
        }
        
        //-- Refresh all connected vertices --
        //clean up
        OutVertices.Clear();
        
        //iterate over all edges that are linked to this vertex...
        foreach (Edge edge in linkedEdges)
        {
            //if this vertex is the source of an edge, we can travel to the target vertex, so add it to the connected vertices list
            if (edge.Source == this)
                OutVertices.Add(edge.Target);
            
            //if this vertex is not the source, but the target ...
            else if(edge.Target == this)
                //and the edge is not directed ...
                if (!edge.IsDirected)
                    //we can travel to the source vertex, so add it to the connected vertices list
                    OutVertices.Add(edge.Source);
        }
    }

    public override string ToString()
    {
        return Name;
    }
}