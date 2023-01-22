//(c) copyright by Martin M. Klöckener
using System.Globalization;

namespace ConsoleAdventure;
public class Dungeon
{
    public List<Room> Rooms;
    public List<Door> Doors;

    //Constructor
    public Dungeon()
    {
        Rooms = new List<Room>();
        Doors = new List<Door>();
    }
    
    public void AddRoom(Room room)
    {
        //add new room to the graph
        Rooms.Add(room);
    }

    public void AddDoor(Door door)
    {
        //add door to the graph
        Doors.Add(door);
        
        //let the source room know of the door
        door.Source.RegisterDoor(door);
        
        //let the target room know of the door
        door.Target.RegisterDoor(door);
    }

    public void RemoveRoom(Room room)
    {
        //if the room is not part of the graph...
        if (!Rooms.Contains(room))
        {
            //let the user know and don't do anything
            Console.WriteLine($"Can't remove room \"{room.ToString()}\" because it's not part of this graph!");
            return;
        }

        //iterate backwards over all doors of the graph...
        for (int i = Doors.Count - 1; i >= 0; i--)
        {
            //... and if the door is linked to the room we want to delete...
            if(Doors[i].IncludesRoom(room))
                //... also delete the door.
                RemoveDoor(Doors[i]);
        }

        //finally remove the room from the graph
        Rooms.Remove(room);
    }

    public void RemoveDoor(Door door)
    {
        //let the source room know that this door will be removed
        door.Source.UnregisterDoor(door);
        
        //let the target room know that this door will be removed
        door.Target.UnregisterDoor(door);

        //remove the door from the graph
        Doors.Remove(door);
    }

    public bool RoomExists(RoomPosition position)
    {
        foreach (Room room in Rooms)
        {
            if (room.Position == position) return true;
        }

        return false;
    }

    public bool RoomExists(uint x, uint y) => RoomExists(new RoomPosition(x, y));

    public bool DoorExists(Room room1, Room room2)
    {
        foreach (Door door in Doors)
        {
            if ((door.Source == room1 && door.Target == room2) ||
                (door.Source == room2 && door.Target == room1))
                
                return true;
        }

        return false;
    }

    public Room GetRoomByPosition(RoomPosition position)
    {
        foreach (Room room in Rooms)
        {
            if (room.Position == position) return room;
        }

        return null;
    }

    public Room GetRoomByPosition(uint x, uint y) => GetRoomByPosition(new RoomPosition(x, y));

    public override string ToString()
    {
        string output = string.Empty;

        //format rooms - format: Rooms = {a, b, c}
        output += "Rooms = {";

        for (int i = 0; i < Rooms.Count; i++)
        {
            output += Rooms[i].ToString();
            if (i < Rooms.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //format doors - format: E = {(a, b), {a, c}, (c, b)}   (round braces for directed, curly braces for non-directed doors)
        output += "Doors = {";

        for (int i = 0; i < Doors.Count; i++)
        {
            output += Doors[i].ToString();
            if (i < Doors.Count - 1) output += ", ";
        }

        output += "}\n";

        //return result
        return output;
    }
}