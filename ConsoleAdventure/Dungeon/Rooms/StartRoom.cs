//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public class StartRoom : Room
{
    private bool _isStartOfAdventure = true;
    protected override string EnterText =>
        _isStartOfAdventure
            ? "You go down an old stone staircase and after a while you reach a small room. No one seems to have been here for a long time. A shiver runs down your spine when you notice the stone statues staring at you with dead eyes. You clutch your knife. Then you decide to set off into the unknown."
            : "In this room is the entrance to the dungeon. This is where your adventure started. You didn't come back to give up, did you?";
   
    public StartRoom(RoomPosition position) : base("Start Room", position)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _isStartOfAdventure = false;
        throw new NotImplementedException();
    }
}