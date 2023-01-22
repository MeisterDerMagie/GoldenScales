//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public class TraderRoom : Room
{
    protected override string EnterText =>
        "You enter a room where a merchant offers his goods. Down here in the old walls? Strange... But you accept it with thanks!";
    public override string RoomMapIcon => Constants.MapRoomTrader;

    public TraderRoom(RoomPosition position) : base("Trader Room", position)
    {
    }

    public override void Enter()
    {
        base.Enter();
        throw new NotImplementedException();
    }
}