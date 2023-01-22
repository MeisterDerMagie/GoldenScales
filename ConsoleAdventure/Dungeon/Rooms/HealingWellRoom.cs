//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public class HealingWellRoom : Room
{
    public bool HasBeenUsed;
    public override string RoomMapIcon => HasBeenUsed ? Constants.MapRoom : Constants.MapRoomHealingWell;
    protected override string EnterText =>
        HasBeenUsed
            ? "The magic well in this room is exhausted after you have already healed your wounds here."
            : "The room you enter exudes a wondrous aura. In the center is a magical well, whose water heals all wounds. Would you like to drink from it now? If not, you can always come back later.";
    
    public HealingWellRoom(RoomPosition position) : base("Healing Well Room", position)
    {
    }

    public override void Enter()
    {
        base.Enter();
        throw new NotImplementedException();
    }
}