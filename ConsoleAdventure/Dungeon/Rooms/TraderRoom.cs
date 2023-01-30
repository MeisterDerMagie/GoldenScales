//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;
using ConsoleAdventure.NPCs;
// ReSharper disable InconsistentNaming

namespace ConsoleAdventure.Rooms;

public class TraderRoom : Room
{
    protected override string EnterText =>
        "You enter a room where a merchant offers his goods. Down here in the old walls? Strange... But you accept it with thanks!";
    public override string RoomMapIcon => Constants.MapRoomTrader;

    private Trader _trader;

    public TraderRoom(RoomPosition position) : base("Trader Room", position)
    {
        //create trader dialog
        string name = NPCNames.GetRandomNPCName();

        var dialog = new DialogNode("NONE", $"Welcome, my good fellow! I am {name}, what can I serve you with?", null);
        DialogNode Node_1_1 = dialog.AddChild( "What are you doing down here? I can hardly imagine that many customers come by here. Wouldn't you be better off selling your goods at the market in town?", 
                                                "Well, you're down here too. You wouldn't believe how many \"adventurers\" are hot for the treasure of the lindworm. And so far, no one has been able to defeat him. That means there's a lot of valuables accumulating down here over time, if you know what I mean.... The business is filthier than a market stall, but all the more rewarding because there is less competition.");
        //DialogNode Node_1_2 = dialog.AddChild();
        
        //generate inventory
        var inventory = new List<Item>();
        var rng = new Random();
        int itemAmount = rng.Next(Constants.TraderItemAmount.Minimum, Constants.TraderItemAmount.Maximum + 1);
        
        for (int i = 0; i < itemAmount; i++)
        {
            int itemValue = rng.Next(Constants.TraderItemValue.Minimum, Constants.TraderItemValue.Maximum + 1);
            Item item = ItemFactory.GenerateRandomTraderItem(itemValue);
            inventory.Add(item);
        }

        _trader = new Trader(name, dialog, inventory);
    }

    public override void Enter()
    {
        base.Enter();
        throw new NotImplementedException();
    }
}