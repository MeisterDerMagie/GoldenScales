//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Room
{
    public string Name;
    public RoomPosition Position;
    private readonly HashSet<Door> linkedDoors; //all linked doors, regardless of if they are directional or non-directional
    private readonly HashSet<Room> linkedRooms; //all linked rooms, regardless of if they are connected with directional or non-directional doors
    
    /// <summary> All out-doors. Does not include "incoming" directed doors. </summary>
    public readonly HashSet<Door> OutDoors;
    
    /// <summary>All rooms that can be travelled to from this room. Takes directional doors into consideration. </summary>
    public readonly HashSet<Room> OutRooms;

    //Constructor
    public Room(string name, RoomPosition position)
    {
        Name = name;
        Position = position;
        linkedDoors = new HashSet<Door>();
        linkedRooms = new HashSet<Room>();
        OutDoors = new HashSet<Door>();
        OutRooms = new HashSet<Room>();
    }

    //if a new door got added, we need to also reference it inside the linked rooms
    public void RegisterDoor(Door door)
    {
        if (!door.IncludesRoom(this))
        {
            Console.WriteLine($"Can't register the door {door.ToString()} because it's not linked to this room {this.ToString()}!");
            return;
        }
        
        linkedDoors.Add(door);
        Update();
    }

    //if an door got delted, we need to also remove the reference from the lists in this room
    public void UnregisterDoor(Door door)
    {
        if (!door.IncludesRoom(this))
        {
            Console.WriteLine($"Can't unregister the door {door.ToString()} because it's not linked to this room {this.ToString()}!");
            return;
        }

        linkedDoors.Remove(door);
        
        Update();
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
            if(door.Source == this)
                //... add the target to the linked rooms
                linkedRooms.Add(door.Target);
            
            //if this room is the target of an door ...
            else if(door.Target == this)
                //... add the source to the linked rooms
                linkedRooms.Add(door.Source);
        }
        
        //-- Refresh all connected doors --
        //clean up
        OutDoors.Clear();
        
        //iterate over all doors that are linked to this room...
        foreach (Door door in linkedDoors)
        {
            //if this room is the target of an door...
            if(door.Target == this)
                //... and if the door is directed ...
                if(door.IsDirected)
                    //... don't add it to the connected doors list
                    continue;
            
            //otherwise add the door
            OutDoors.Add(door);
        }
        
        //-- Refresh all connected rooms --
        //clean up
        OutRooms.Clear();
        
        //iterate over all doors that are linked to this room...
        foreach (Door door in linkedDoors)
        {
            //if this room is the source of an door, we can travel to the target room, so add it to the connected rooms list
            if (door.Source == this)
                OutRooms.Add(door.Target);
            
            //if this room is not the source, but the target ...
            else if(door.Target == this)
                //and the door is not directed ...
                if (!door.IsDirected)
                    //we can travel to the source room, so add it to the connected rooms list
                    OutRooms.Add(door.Source);
        }
    }

    public override string ToString()
    {
        return Name;
    }
}