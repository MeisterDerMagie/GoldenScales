//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public class ChestRoom : Room
{
    public bool HasBeenLooted;
    protected override string EnterText =>
        HasBeenLooted
            ? "You already looted the chest in this room. Hopefully no one misses his belongings now..." 
            : "You open the door and spot a chest in the corner of the room. Do you open it?";
    
    public ChestRoom(RoomPosition position) : base("Chest Room", position)
    {
    }

    public override void Enter()
    {
        base.Enter();
        throw new NotImplementedException();
    }
}