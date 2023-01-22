//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public class TrapRoom : Room
{
    public bool TrapHasBeenTriggered;
    protected override string EnterText =>
        TrapHasBeenTriggered
            ? "In this room, you had stepped into a trap. Your blood is still on the spikes, but the mechanism seems to trigger only once."
            : "You enter a room that has nothing to offer except dust and ... aaargh! Ouch! You got hit by a hidden trap. How mean! Who hides traps in their basement?";

    public TrapRoom(RoomPosition position) : base("Trap Room", position)
    {
    }
    

    public override void Enter()
    {
        base.Enter();
        throw new NotImplementedException();
    }
}