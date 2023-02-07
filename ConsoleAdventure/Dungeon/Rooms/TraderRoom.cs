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
        //add room commands
        var talkCommand = new Command("talk (talk to the trader)", new List<string> { "talk", "trade"}, StartDialog);
        var attackComand = new Command("attack", new List<string> { "attack", "kill" }, AttackTrader, true);
        RoomCommands.Add(talkCommand);
        RoomCommands.Add(attackComand);
        
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
        StartDialog();
    }

    private void StartDialog()
    {
        var dialogState = new Dialog(_trader);
        Game.StateMachine.SetState(dialogState);
    }

    private void AttackTrader()
    {
        Console.WriteLine("In a burst of madness, you attack the merchant. Reflexively, he pulls a chain hanging on the wall behind him. Before you know it, a huge boulder crashes out of the ceiling and crushes you. Apparently, the merchant knew exactly what kind of shady people he would have to deal with down here...");
        Player.Singleton.DealDamage(100);
    }
}