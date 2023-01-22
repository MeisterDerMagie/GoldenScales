//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public class BossRoom : Room
{
    protected override string EnterText =>
        "You enter a room full of treasures. Only at second glance do you realize that some of the golden treasures are actually shiny scales. Before you know it, a gigantic lindworm rears up in front of you, defending its belongings. Get ready for an epic battle!";
    public override string RoomMapIcon => Constants.MapRoomBoss;

    public BossRoom(RoomPosition position) : base("Boss Room", position)
    {
    }

    public override void Enter()
    {
        base.Enter();
        throw new NotImplementedException();
    }
}