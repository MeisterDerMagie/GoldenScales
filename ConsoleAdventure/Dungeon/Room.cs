//(c) copyright by Martin M. Klöckener
using System.Numerics;

namespace ConsoleAdventure;
public abstract class Room
{
    public string Name;
    public RoomPosition Position;
    public readonly HashSet<Door> LinkedDoors; //all linked doors
    public readonly HashSet<Room> LinkedRooms; //all linked rooms

    public bool Discovered { get; private set; }
    protected abstract string EnterText { get; }

    public virtual string RoomMapIcon => Constants.MapRoom;

    public virtual void Enter()
    {
        Discover();
        Console.WriteLine(EnterText);
    }

    public void Discover()
    {
        Discovered = true;
        foreach (Door door in LinkedDoors)
        {
            door.Discovered = true;
        }
    }

    public bool HasDoorAt(Direction direction)
    {
        foreach (Door door in LinkedDoors)
        {
            Room otherRoom = door.Source == this ? door.Target : door.Source;

            switch (direction)
            {
                case Direction.North:
                    if(Position.North == otherRoom.Position) return true;
                    break;
                case Direction.East:
                    if(Position.East == otherRoom.Position) return true;
                    break;
                case Direction.South:
                    if(Position.South == otherRoom.Position) return true;
                    break;
                case Direction.West:
                    if(Position.West == otherRoom.Position) return true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        return false;
    }

    public Room GetAdjacentRoomAt(Direction direction)
    {
        foreach (Door door in LinkedDoors)
        {
            Room otherRoom = door.Source == this ? door.Target : door.Source;
            
            switch (direction)
            {
                case Direction.North:
                    if(Position.North == otherRoom.Position) return otherRoom;
                    break;
                case Direction.East:
                    if(Position.East == otherRoom.Position) return otherRoom;
                    break;
                case Direction.South:
                    if(Position.South == otherRoom.Position) return otherRoom;
                    break;
                case Direction.West:
                    if(Position.West == otherRoom.Position) return otherRoom;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
        
        return null;
    }

    //Constructor
    public Room(string name, RoomPosition position)
    {
        Name = name;
        Position = position;
        LinkedDoors = new HashSet<Door>();
        LinkedRooms = new HashSet<Room>();
    }

    //if a new door got added, we need to also reference it inside the linked rooms
    public void RegisterDoor(Door door)
    {
        if (!door.IncludesRoom(this))
        {
            Console.WriteLine($"Can't register the door {door.ToString()} because it's not linked to this room {this.ToString()}!");
            return;
        }
        
        LinkedDoors.Add(door);
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

        LinkedDoors.Remove(door);
        
        Update();
    }

    //Update the linked rooms, out-doors and out-rooms
    private void Update()
    {
        //-- Refresh all linked rooms --
        //clean up
        LinkedRooms.Clear();

        //iterate over all doors that are linked to this room...
        foreach (Door door in LinkedDoors)
        {
            //if this room is the source of an door ...
            if(door.Source == this)
                //... add the target to the linked rooms
                LinkedRooms.Add(door.Target);
            
            //if this room is the target of an door ...
            else if(door.Target == this)
                //... add the source to the linked rooms
                LinkedRooms.Add(door.Source);
        }
    }

    public override string ToString()
    {
        return Position.ToString();
    }
}