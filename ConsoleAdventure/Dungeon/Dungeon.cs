//(c) copyright by Martin M. Klöckener
using System.Globalization;

namespace ConsoleAdventure;
public class Dungeon
{
    public List<Room> rooms;
    public List<Door> doors;

    //Constructor
    public Dungeon()
    {
        rooms = new List<Room>();
        doors = new List<Door>();
    }
    
    public void AddRoom(Room _room)
    {
        //add new room to the graph
        rooms.Add(_room);
    }

    public void AddDoor(Door _door)
    {
        //add door to the graph
        doors.Add(_door);
        
        //let the source room know of the door
        _door.source.RegisterDoor(_door);
        
        //let the target room know of the door
        _door.target.RegisterDoor(_door);
    }

    public void RemoveRoom(Room _room)
    {
        //if the room is not part of the graph...
        if (!rooms.Contains(_room))
        {
            //let the user know and don't do anything
            Console.WriteLine($"Can't remove room \"{_room.ToString()}\" because it's not part of this graph!");
            return;
        }

        //iterate backwards over all doors of the graph...
        for (int i = doors.Count - 1; i >= 0; i--)
        {
            //... and if the door is linked to the room we want to delete...
            if(doors[i].IncludesRoom(_room))
                //... also delete the door.
                RemoveDoor(doors[i]);
        }

        //finally remove the room from the graph
        rooms.Remove(_room);
    }

    public void RemoveDoor(Door _door)
    {
        //let the source room know that this door will be removed
        _door.source.UnregisterDoor(_door);
        
        //let the target room know that this door will be removed
        _door.target.UnregisterDoor(_door);

        //remove the door from the graph
        doors.Remove(_door);
    }

    public override string ToString()
    {
        string output = string.Empty;

        //format rooms - format: V = {a, b, c}
        output += "V = {";

        for (int i = 0; i < rooms.Count; i++)
        {
            output += rooms[i].ToString();
            if (i < rooms.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //format doors - format: E = {(a, b), {a, c}, (c, b)}   (round braces for directed, curly braces for non-directed doors)
        output += "E = {";

        for (int i = 0; i < doors.Count; i++)
        {
            output += doors[i].ToString();
            if (i < doors.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //format weights - format: W = {{(a, b), 0.5}, {{a, c}, 0.33}}
        output += "W = {";

        for (int i = 0; i < doors.Count; i++)
        {
            output += "{";
            output += doors[i].ToString();
            output += ", ";
            output += Math.Round(doors[i].weight, 1).ToString(CultureInfo.InvariantCulture);
            output += "}";
            
            if (i < doors.Count - 1) output += ", ";
        }

        output += "}\n";
        
        //return result
        return output;
    }
}