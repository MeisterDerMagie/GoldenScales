//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Vertex
{
    public string name;
    public Vector2 position;
    private HashSet<Edge> linkedEdges; //all linked edges, regardless of if they are directional or non-directional
    private HashSet<Vertex> linkedVertices; //all linked vertices, regardless of if they are connected with directional or non-directional edges
    
    /// <summary> All out-edges. Does not include "incoming" directed edges. </summary>
    public HashSet<Edge> outEdges;
    
    /// <summary>All vertices that can be travelled to from this vertex. Takes directional edges into consideration. </summary>
    public HashSet<Vertex> outVertices;

    //Constructor
    public Vertex(string _name, Vector2 _position)
    {
        name = _name;
        position = _position;
        linkedEdges = new HashSet<Edge>();
        linkedVertices = new HashSet<Vertex>();
        outEdges = new HashSet<Edge>();
        outVertices = new HashSet<Vertex>();
    }

    //if a new edge got added, we need to also reference it inside the linked vertices
    public void RegisterEdge(Edge _edge)
    {
        if (!_edge.IncludesVertex(this))
        {
            Console.WriteLine($"Can't register the edge {_edge.ToString()} because it's not linked to this vertex {this.ToString()}!");
            return;
        }
        
        linkedEdges.Add(_edge);
        Update();
    }

    //if an edge got delted, we need to also remove the reference from the lists in this vertex
    public void UnregisterEdge(Edge _edge)
    {
        if (!_edge.IncludesVertex(this))
        {
            Console.WriteLine($"Can't unregister the edge {_edge.ToString()} because it's not linked to this vertex {this.ToString()}!");
            return;
        }

        linkedEdges.Remove(_edge);
        
        Update();
    }

    public void SetPosition(Vector2 _newPos)
    {
        //change the position of this vertex
        position = _newPos;

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
            if(edge.source == this)
                //... add the target to the linked vertices
                linkedVertices.Add(edge.target);
            
            //if this vertex is the target of an edge ...
            else if(edge.target == this)
                //... add the source to the linked vertices
                linkedVertices.Add(edge.source);
        }
        
        //-- Refresh all connected edges --
        //clean up
        outEdges.Clear();
        
        //iterate over all edges that are linked to this vertex...
        foreach (Edge edge in linkedEdges)
        {
            //if this vertex is the target of an edge...
            if(edge.target == this)
                //... and if the edge is directed ...
                if(edge.isDirected)
                    //... don't add it to the connected edges list
                    continue;
            
            //otherwise add the edge
            outEdges.Add(edge);
        }
        
        //-- Refresh all connected vertices --
        //clean up
        outVertices.Clear();
        
        //iterate over all edges that are linked to this vertex...
        foreach (Edge edge in linkedEdges)
        {
            //if this vertex is the source of an edge, we can travel to the target vertex, so add it to the connected vertices list
            if (edge.source == this)
                outVertices.Add(edge.target);
            
            //if this vertex is not the source, but the target ...
            else if(edge.target == this)
                //and the edge is not directed ...
                if (!edge.isDirected)
                    //we can travel to the source vertex, so add it to the connected vertices list
                    outVertices.Add(edge.source);
        }
    }

    public override string ToString()
    {
        return name;
    }
}