//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Enemies;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure.Rooms;

public class EnemyRoom : Room
{
    public bool EnemyHasBeenDefeated;
    public Enemy Enemy;
    
    protected override string EnterText =>
        EnemyHasBeenDefeated
            ? $"You enter a room where the remains of a {Enemy.Name} lay on the ground. What a glorious victory of yours!"
            : $"When you enter the room, you are immediately attacked by a {Enemy.Name}. Get ready for a fight!";

    public EnemyRoom(RoomPosition position) : base("Enemy Room", position)
    {
        int random = RandomUtilities.RandomInt(0, 3);
        Enemy = random switch
        {
            0 => new Skeleton(),
            1 => new Spider(),
            2 => new Wizard(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public override void Enter()
    {
        base.Enter();
        
        //begin fight
        var fight = new Fight(Player.Singleton, Enemy);
        Game.StateMachine.SetState(fight);
    }
}