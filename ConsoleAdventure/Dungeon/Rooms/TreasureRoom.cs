//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public class TreasureRoom : Room
{
    public bool HasBeenLooted;

    protected override string EnterText =>
        HasBeenLooted
            ? "You enter a treasure room, from which, however, you have already taken everything of importance."
            : "You enter a room full of treasures! What luck! You keep your eyes open and look for the Lindworm lurking here, but you see no danger. You can only dream of how great the treasure must be that the Lindworm guards, if this gold is lying around here unclaimed.";
    
    public TreasureRoom(RoomPosition position) : base("Treasure Room", position)
    {
    }


    public override void Enter()
    {
        base.Enter();
        throw new NotImplementedException();
    }
}