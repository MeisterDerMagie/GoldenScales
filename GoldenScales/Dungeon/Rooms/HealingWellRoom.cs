//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Utilities;

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
        var drinkCommand = new Command("drink (drink from the well to heal all your wounds)", new List<string> { "drink"}, Drink);
        RoomCommands.Add(drinkCommand);
    }

    public override void Enter()
    {
        base.Enter();
        
        //if the well was already used, do nothing
        if (HasBeenUsed) return;
        
        //heal player if they want to
        bool useNow = ConsoleUtilities.InputBoolean("Drink from the well now?");
        if (useNow)
        {
            Drink();
        }
        else
        {
            bool playerHasFullHealth = Player.Singleton.Health == Player.Singleton.MaxHealth;
            string output = playerHasFullHealth ? "You decide not to drink from the well yet. A wise decision, because you have full health. Maybe you will come back at a later time?" : "You decide not to drink from the well yet. Maybe you will come back at a later time?";
            Console.WriteLine(output);
        }
    }

    private void Drink()
    {
        if (HasBeenUsed)
        {
            Console.WriteLine("You already drank from this well. It ran dry, so there is no water left to drink.");
            return;
        }
        
        //feedback
        Console.WriteLine("You drink from the magical well, which then runs dry.");
        
        //heal player fully
        Player.Singleton.HealFully();
        
        //set room to used
        HasBeenUsed = true;
        
        //remove the drink command and replace it with a hidden one (it will not be shown in the list but if a players calls it anyways, they get a feedback that the well ran dry)
        RoomCommands.Clear();
        var hiddenDisabledDrinkCommand = new Command("drink", new List<string> { "drink"}, Drink, true);
        RoomCommands.Add(hiddenDisabledDrinkCommand);
    }
}