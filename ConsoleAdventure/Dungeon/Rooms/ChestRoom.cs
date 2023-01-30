//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure.Rooms;

public class ChestRoom : Room
{
    public bool HasBeenLooted;
    protected override string EnterText =>
        HasBeenLooted
            ? "You already looted the chest in this room. Hopefully no one misses his belongings now..." 
            : "You open the door and spot a chest in the corner of the room. Do you open it?";
    
    public ChestRoom(RoomPosition position) : base("Chest Room", position)
    {
        var openChestCommand = new Command("open chest (open the chest to loot it)", new List<string> { "open chest", "open"}, Open);
        RoomCommands.Add(openChestCommand);
    }

    public override void Enter()
    {
        base.Enter();
        
        //if the chest has already been opened, do nothing
        if (HasBeenLooted) return;
        
        //heal player if they want to
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
        if (HasBeenLooted)
        {
            Console.WriteLine("You have already opened this chest. Except for some woodlice and some sand, there is nothing else in it.");
            return;
        }
        
        //generate random item
        var rng = new Random();
        int goldValue = rng.Next(Constants.ChestValueRange.Minimum, Constants.ChestValueRange.Maximum + 1);

        Item randomItem = ItemFactory.GenerateRandomLootItem(goldValue);
        
        //feedback
        string valueFeedback = string.Empty;
        if (goldValue == Constants.ChestValueRange.Minimum)
            valueFeedback = (randomItem is Consumable) ? "It won't be of much use, but at least it's better than nothing." :"It looks pretty shabby. Who keeps something like this in an ornate chest?";
        else if (goldValue - Constants.ChestValueRange.Minimum <= (Constants.ChestValueRange.Maximum - Constants.ChestValueRange.Minimum) / 2f)
            valueFeedback = (randomItem is Consumable) ? "You'll be able to put that to good use." : "It doesn't look particularly valuable, but you'll certainly find a use for it.";
        else if (goldValue < Constants.ChestValueRange.Maximum)
            valueFeedback = (randomItem is Consumable) ? "That is useful! Good for you that someone stashed this item here!" : "It looks reasonably valuable. Good for you that someone stashed this item here!";
        else
            valueFeedback = "Awesome, this is a valuable item! Luck was with you. Hopefully you can put the item to good use or sell it for a decent sum.";
        
        Console.WriteLine($"You open the chest and find a {randomItem.Name}. {valueFeedback} You put it in your inventory.");
        
        //put item in inventory
        Player.Singleton.AddToIventory(randomItem, silently:true);
        
        //set room to used
        HasBeenLooted = true;
        
        //remove the open command and replace it with a hidden one (it will not be shown in the list but if a players calls it anyways, they get a feedback that they already openened the chest)
        RoomCommands.Clear();
        var hiddenDisabledOpenCommand = new Command("open", new List<string> { "open chest", "open"}, Open, true);
        RoomCommands.Add(hiddenDisabledOpenCommand);
    }
}