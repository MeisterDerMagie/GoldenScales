//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Room
{
    public string name;
    public Vector2 position;
    private HashSet<Door> linkedDoors; //all linked doors, regardless of if they are directional or non-directional
    private HashSet<Room> linkedRooms; //all linked rooms, regardless of if they are connected with directional or non-directional doors
    
    /// <summary> All out-doors. Does not include "incoming" directed doors. </summary>
    public HashSet<Door> outDoors;
    
    /// <summary>All rooms that can be travelled to from this room. Takes directional doors into consideration. </summary>
    public HashSet<Room> outRooms;

    //Constructor
    public Room(string _name, Vector2 _position)
    {
        name = _name;
        position = _position;
        linkedDoors = new HashSet<Door>();
        linkedRooms = new HashSet<Room>();
        outDoors = new HashSet<Door>();
        outRooms = new HashSet<Room>();
    }

    //if a new door got added, we need to also reference it inside the linked rooms
    public void RegisterDoor(Door _door)
    {
        if (!_door.IncludesRoom(this))
        {
            Console.WriteLine($"Can't register the door {_door.ToString()} because it's not linked to this room {this.ToString()}!");
            return;
        }
        
        linkedDoors.Add(_door);
        Update();
    }

    //if an door got delted, we need to also remove the reference from the lists in this room
    public void UnregisterDoor(Door _door)
    {
        if (!_door.IncludesRoom(this))
        {
            Console.WriteLine($"Can't unregister the door {_door.ToString()} because it's not linked to this room {this.ToString()}!");
            return;
        }

        linkedDoors.Remove(_door);
        
        Update();
    }

    public void SetPosition(Vector2 _newPos)
    {
        //change the position of this room
        position = _newPos;

        //let all doors that are linked to this room know about the position change, so they can update the weight (distance) accordingly
        foreach (Door door in linkedDoors)
        {
            door.UpdateWeight();
        }
    }
    
    //Update the linked rooms, out-doors and out-rooms
    private void Update()
    {
        //-- Refresh all linked rooms --
        //clean up
        linkedRooms.Clear();

        //iterate over all doors that are linked to this room...
        foreach (Door door in linkedDoors)
        {
            //if this room is the source of an door ...
            if(door.source == this)
                //... add the target to the linked rooms
                linkedRooms.Add(door.target);
            
            //if this room is the target of an door ...
            else if(door.target == this)
                //... add the source to the linked rooms
                linkedRooms.Add(door.source);
        }
        
        //-- Refresh all connected doors --
        //clean up
        outDoors.Clear();
        
        //iterate over all doors that are linked to this room...
        foreach (Door door in linkedDoors)
        {
            //if this room is the target of an door...
            if(door.target == this)
                //... and if the door is directed ...
                if(door.isDirected)
                    //... don't add it to the connected doors list
                    continue;
            
            //otherwise add the door
            outDoors.Add(door);
        }
        
        //-- Refresh all connected rooms --
        //clean up
        outRooms.Clear();
        
        //iterate over all doors that are linked to this room...
        foreach (Door door in linkedDoors)
        {
            //if this room is the source of an door, we can travel to the target room, so add it to the connected rooms list
            if (door.source == this)
                outRooms.Add(door.target);
            
            //if this room is not the source, but the target ...
            else if(door.target == this)
                //and the door is not directed ...
                if (!door.isDirected)
                    //we can travel to the source room, so add it to the connected rooms list
                    outRooms.Add(door.source);
        }
    }

    public override string ToString()
    {
        return name;
    }
}