//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;
using ConsoleAdventure.NPCs;

namespace ConsoleAdventure.Rooms;

public class TraderRoom : Room
{
    protected override string EnterText =>
        "You enter a room where a merchant offers his goods. Down here in the old walls? Strange... But you accept it with thanks!";
    public override string RoomMapIcon => Constants.MapRoomTrader;

    private Trader _trader;

    public TraderRoom(RoomPosition position) : base("Trader Room", position)
    {
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
        
        //create trader
        string name = NPCNames.GetRandomNPCName();
        _trader = new Trader(name, inventory);

        //create dialog
        DialogNode dialog = DialogFactory.GetTraderDialog(_trader);
        _trader.SetDialog(dialog);
    }

    public override void Enter()
    {
        base.Enter();

        //start dialog
        var dialogState = new Dialog(_trader);
        Game.StateMachine.SetState(dialogState);
    }
}