//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public class Room
{
    public string Name;
    public RoomPosition Position;
    public readonly HashSet<Door> linkedDoors; //all linked doors
    public readonly HashSet<Room> linkedRooms; //all linked rooms

    public bool IsBossRoom;

    //Constructor
    public Room(string name, RoomPosition position)
    {
        Name = name;
        Position = position;
        linkedDoors = new HashSet<Door>();
        linkedRooms = new HashSet<Room>();
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

    //if a door got deleted, we need to also remove the reference from the lists in this room
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
    }

    public override string ToString()
    {
        return Position.ToString();
    }
}