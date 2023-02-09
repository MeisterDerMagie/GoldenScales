//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Enemies;
using ConsoleAdventure.Items;
using ConsoleAdventure.Utilities;

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
        var openChestCommand = new Command("open chest (open the chest to loot it)", new List<string> { "open chest", "open"}, Open);
        RoomCommands.Add(openChestCommand);

        Enemy = new Mimic();
    }

    public override void Enter()
    {
        base.Enter();
        
        //if the mimic already has been defeated, do nothing
        if (EnemyHasBeenDefeated) return;
        
        //open the chest (a.k.a trigger mimic fight) if the player wants to
        bool openNow = ConsoleUtilities.InputBoolean("Open the chest now?");
        if (openNow)
        {
            Open();
        }
        else
        {
            string output = "You decide not to open the chest right now. Who knows what treasures will slip through your fingers. You can return later and open the chest then.";
            Console.WriteLine(output);
        }
    }
    
    private void Open()
    {
        //let the player know that they triggered a fight with a mimic
        Console.WriteLine("The moment you are about to open the chest, you notice that there is something strange about it. It breathes. It's a Mimic! But by then it's already too late. The mimic opens its stinking mouth and snaps at you with its thousand teeth. The fight begins!");
        
        //begin fight
        var fight = new Fight(Enemy);
        Game.StateMachine.SetState(fight);
        
        //set enemy to defeated (you currently can't flee from a fight, so either you defeat the enemy or die)
        EnemyHasBeenDefeated = true;
        
        //remove the open command
        RoomCommands.Clear();
    }
}