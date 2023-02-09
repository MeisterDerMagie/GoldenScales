//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Utilities;

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

        //do nothing if the trap has been triggered before
        if (TrapHasBeenTriggered) return;
        
        //deal trap damage
        var rng = new Random();
        int damage = rng.Next(Constants.TrapDamage.Minimum, Constants.TrapDamage.Maximum + 1);
        Player.Singleton.DealDamage(damage);
        
        //set trap triggered
        TrapHasBeenTriggered = true;
        
        //output different feedback depending on the amount of damage the trap dealt
        string output;
        if (damage == Constants.TrapDamage.Minimum)
            output = "This was a tiny trap. It still hurt a bit, but it could have been much worse. Lucky!";
        
        else if (damage - Constants.TrapDamage.Minimum <= (Constants.TrapDamage.Maximum - Constants.TrapDamage.Minimum) / 3f)
            output = "The trap only hit you in the shoulder. It hurt, but if you had been standing a little further to the right, it would have dropped on your head. You were lucky!";
        
        else if (damage - Constants.TrapDamage.Minimum <= (Constants.TrapDamage.Maximum - Constants.TrapDamage.Minimum) / 3f * 2f)
            output = "That hurt! You free yourself from the trap. On we go!";
        
        else if (damage < Constants.TrapDamage.Maximum)
            output = "Damn, that was a heavy hit. You need a moment to free yourself from the trap and get back on your feet. That was bad luck.";
        
        else
            output = "Bull's eye! The trap hit you in the head and crushed you. With pain you manage to free yourself. This will leave big scars ... provided you make it out of this miserable dungeon alive.";
            
        Console.WriteLine(output);
    }
}