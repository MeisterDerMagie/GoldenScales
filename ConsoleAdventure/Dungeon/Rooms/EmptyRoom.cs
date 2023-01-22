//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public class EmptyRoom : Room
{
    protected override string EnterText =>
        "You enter a room that has nothing to offer except dust and cobwebs. How boring.";
    
    public EmptyRoom(RoomPosition position) : base("Empty Room", position)
    {
    }

    public override void Enter()
    {
        base.Enter();
        throw new NotImplementedException();
    }
}