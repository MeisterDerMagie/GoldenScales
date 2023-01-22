//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Enemies;

namespace ConsoleAdventure.Rooms;

public class MimicRoom : Room
{
    public bool EnemyHasBeenDefeated;
    public Enemy Enemy;
    
    protected override string EnterText =>
        EnemyHasBeenDefeated
            ? "In this room lie the remains of the Mimic. What a devious creature! You are glad to have defeated this monster."
            : "You open the door and spot a chest in the corner of the room. Do you open it?";
    
    public MimicRoom(RoomPosition position) : base("Mimic Room", position)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        throw new NotImplementedException();
    }
}