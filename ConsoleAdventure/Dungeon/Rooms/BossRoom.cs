//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Enemies;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure.Rooms;

public class BossRoom : Room
{
    private bool _firstTimeEntering = true;
    protected override string EnterText =>
        _firstTimeEntering
            ? "You enter a room full of treasures. Only at second glance do you realize that some of the golden treasures are actually shiny scales. Before you know it, the gigantic Lindworm rears up in front of you, defending its belongings. You are still standing close to the door. You can either fight the Lindworm now or quickly return to the previous room to fight later."
            : "You are back in the room of the Lindworm. The golden treasures glitter enticingly! Have you returned to fight and take the treasures?";
    public override string RoomMapIcon => Constants.MapRoomBoss;

    public BossRoom(RoomPosition position) : base("Boss Room", position)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _firstTimeEntering = false;
     
        //ask player if they want to fight now or return to the previous room
        bool startFight = ConsoleUtilities.InputBoolean("Do you want to fight against the Lindworm now?");

        //return to the previous room if the player doesn't want to fight now
        if (!startFight)
        {
            Room previousRoom = Player.Singleton.PreviousRoom;
            Player.Singleton.CurrentRoom = previousRoom;
            previousRoom.Enter();
            return;
        }
        
        //begin fight
        Console.WriteLine("You draw your weapon, determined to defeat the beast. The lindworm seems equally determined to defend its treasure. Anger burns in its eyes, then it tears open its mouth and lunges at you. Get ready for an epic battle!");
        var fight = new Fight(new Lindworm());
        Game.StateMachine.SetState(fight);
    }
}