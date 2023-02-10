//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;
using ConsoleAdventure.NPCs;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Trade : IState
{
    public List<Command> AvailableCommands { get; set; }
    public string TextWhenReturningToThisState => "You continue trading.";

    private Trader _trader;
    
    public Trade(Trader trader)
    {
        _trader = trader;
        AvailableCommands = new List<Command>();
    }


    public void OnEnter()
    {
        //list inventories
        InventoryUtilities.PrintPlayerInventoryTrading(Player.Singleton);
        InventoryUtilities.PrintTraderInventory(_trader, true);
    }

    public void Tick()
    {
        UpdateAvailableCommands();
        
        //wait for user input
        string userInput = ConsoleUtilities.InputString("What do you want to trade (buy/sell)?");
        if (CommandUtilities.TryExecuteUserInput(userInput, AvailableCommands) == false)
        {
            Console.WriteLine("Type \"options\" to see a list of all actions you can do right now.");
        }
    }

    public void OnExit()
    {
        
    }

    private void UpdateAvailableCommands()
    {
        AvailableCommands.Clear();

        var buyItemCommand = new Command("buy + item # (buy an item from the trader, e.g. \"buy 2\")", new List<string>{"buy"}, BuyItem);
        var sellItemCommand = new Command("sell + item # (sell an item from your inventory, e.g. \"sell 2\")", new List<string> { "sell" }, SellItem);
        var listTraderItemsCommand = new Command(
            "list trader items (show all items offered by the trader)",
            new List<string> { "list trader items", "what can i buy", "which items can i buy", "show me your items" },
            () => InventoryUtilities.PrintTraderInventory(_trader, true));
        var listOwnInventory = new Command(
            "list items (show all items in your inventory)",
            new List<string> { "list items", "inventory" },
            () => InventoryUtilities.PrintPlayerInventoryTrading(Player.Singleton));
        var leaveCommand = new Command("leave (stop trading and continue exploring the dungeon)", new List<string> { "leave" }, () => Game.StateMachine.SetState(Game.ExplorationState, false));
        var showGoldCommand = new Command("purse (take a look at your purse to see how much gold you own)", new List<string> { "purse", "gold" }, () => Player.Singleton.PrintPurse());
        var examineCommand = new Command("examine (examine an item in either your or the merchant's inventory.", new List<string> { "examine" }, ExamineItem);
        var totalGoldValueCommand = new Command("total gold value (show the total gold value of all your items)", new List<string>{"total gold value"}, () => Console.WriteLine($"The total gold value of all your items is: {InventoryUtilities.CalculateTotalGoldValueOfAllInventoryItems(Player.Singleton.Inventory)}"));
        var sortTraderInventoryCommand = new Command("sort trader (sort the inventory of the trader by item category and price)",new List<string> { "sort trader" }, () => { InventoryUtilities.SortInventory(_trader.Inventory); InventoryUtilities.PrintTraderInventory(_trader, true); });
        var sortInventoryCommand = new Command("sort (sort your inventory by item category and price)", new List<string> { "sort" }, () => { InventoryUtilities.SortInventory(Player.Singleton.Inventory); InventoryUtilities.PrintPlayerInventoryTrading(Player.Singleton); });
        
        AvailableCommands.Add(buyItemCommand);
        AvailableCommands.Add(sellItemCommand);
        AvailableCommands.Add(listTraderItemsCommand);
        AvailableCommands.Add(listOwnInventory);
        AvailableCommands.Add(leaveCommand);
        AvailableCommands.Add(showGoldCommand);
        AvailableCommands.Add(examineCommand);
        AvailableCommands.Add(totalGoldValueCommand);
        AvailableCommands.Add(sortTraderInventoryCommand);
        AvailableCommands.Add(sortInventoryCommand);
    }
    
    private void BuyItem(List<string> userParameters)
    {
        //can't buy anything if the trader has no more items to sell
        if (_trader.Inventory.Count == 0)
        {
            Console.WriteLine($"You can't buy anything because {_trader.Name} doesn't have any more items to sell.");
            return;
        }

        //try to get the item that the player wants to buy
        (Item item, int itemNumber) = StringParser.InventoryItemFromString(userParameters, _trader.Inventory);
        
        if (item == null)
        {
            Console.WriteLine("Which item do you want to buy? Please type in the form of \"buy 2\", where 2 is the item number in the trader's inventory (type \"list trader items\" to list all items you can buy).");
            return;
        }
        
        //if we managed to get the item, check if the player has enough gold to buy it
        if (Player.Singleton.Gold < item.GoldValue)
        {
            Console.WriteLine($"You don't have enough gold to buy {item.Name}. It costs {item.GoldValue} gold but you only have {Player.Singleton.Gold} in your purse.");
            return;
        }
        
        //buy item
        Player.Singleton.RemoveGold(item.GoldValue, true);
        _trader.Inventory.Remove(item);
        Player.Singleton.AddToIventory(item, true);
        Console.WriteLine($"You buy {item.Name} for {item.GoldValue} gold. You now have {Player.Singleton.Gold} gold left in your purse.");
    }

    private void SellItem(List<string> userParameters)
    {
        //can't sell anything if the doesn't have any items in the inventory
        if (Player.Singleton.Inventory.Count == 0)
        {
            Console.WriteLine($"You can't sell anything because you don't have any items in the inventory.");
            return;
        }

        //try to get the item that the player wants to sell
        (Item item, int itemNumber) = StringParser.InventoryItemFromString(userParameters, Player.Singleton.Inventory);
        
        if (item == null)
        {
            Console.WriteLine("Which item do you want to sell? Please type in the form of \"sell 2\", where 2 is the item number in your inventory (type \"list items\" to list all your items).");
            return;
        }
        
        //if the item is equipped: ask player if they really want to sell it
        var equippable = item as Equippable;
        bool isEquipped = (equippable != null && equippable.IsEquipped);
        if (isEquipped)
        {
            bool reallyUnequip = ConsoleUtilities.InputBoolean($"The item you want to sell is currently equipped at the {equippable.EquipSlot.ToString()} slot. Do you really want to sell it?");
            if (!reallyUnequip)
            {
                Console.WriteLine($"You don't sell your {item.Name}.");
                return;
            }
        }
            
        //if we managed to get the item, sell it (and unequipp it, if it was equipped)
        if (Player.Singleton.RemoveFromInventory(item, true))
        {
            Player.Singleton.AddGold(item.GoldValue, true);
            _trader.Inventory.Add(item);
            string unequipText = isEquipped ? "unequip and " : string.Empty;
            Console.WriteLine($"You {unequipText}sell the {item.Name} for {item.GoldValue} gold and now have a total of {Player.Singleton.Gold} gold in your purse.");
        }
    }

    private void ExamineItem()
    {
        //ask if the player wants to examine an item in their own or the merchant's inventory
        var examineCommands = new List<Command>();
        var examineOwnItem = new Command("own (examine an item in your own inventory)", new List<string>{"own"}, ExamineOwnItem);
        var examineTraderItem = new Command("trader (examine an item in the trader's inventory", new List<string>{"trader"}, ExamineTraderItem);
        examineCommands.Add(examineOwnItem);
        examineCommands.Add(examineTraderItem);

        bool userEnteredValidChoice = false;
        while (!userEnteredValidChoice)
        {
            string userInput = ConsoleUtilities.InputString("Do you want to examine an item in your own inventory or an item offered by the trader? (type \"own\" or \"trader\")");
            userEnteredValidChoice = CommandUtilities.TryExecuteUserInput(userInput, examineCommands);
        }
    }

    private void ExamineOwnItem()
    {
        //can't examine anything if your inventory is empty
        if (Player.Singleton.Inventory.Count == 0)
        {
            Console.WriteLine("You can't examine items from your inventory because you don't own any items.");
            return;
        }
        
        //if there's only one item, examine it and show the stats
        if (Player.Singleton.Inventory.Count == 1)
        {
            Console.WriteLine($"There's only one item in your inventory, the {Player.Singleton.Inventory[0].Name}. You examine it.");
            Player.Singleton.Inventory[0].Examine();
            Console.WriteLine(Player.Singleton.Inventory[0].StatsFull);
            return;
        }
        
        //if there are more items, ask the player which one they want to examine
        InventoryUtilities.PrintPlayerInventory(Player.Singleton);
        int itemNumber = ConsoleUtilities.InputInteger("Which item do you want to examine (enter the item #)?", minValue: 1, maxValue: Player.Singleton.Inventory.Count);
        int itemIndex = itemNumber - 1;
        Item item = Player.Singleton.Inventory[itemIndex];
        Console.WriteLine($"You examine the {item.Name}. (#{itemNumber})");
        item.Examine();
        Console.WriteLine(item.StatsFull);
    }

    private void ExamineTraderItem()
    {
        //can't examine anything if the trader's inventory is empty
        if (_trader.Inventory.Count == 0)
        {
            Console.WriteLine($"You can't examine any items in {_trader.Name}'s inventory because there are no items left.");
            return;
        }
        
        //if there's only one item, examine it and show the stats
        if (_trader.Inventory.Count == 1)
        {
            Console.WriteLine($"There's only one item in {_trader.Name}'s inventory, the {_trader.Inventory[0].Name}. You examine it.");
            _trader.Inventory[0].Examine();
            Console.WriteLine(_trader.Inventory[0].StatsFull);
            return;
        }
        
        //if there are more items, ask the player which one they want to examine
        InventoryUtilities.PrintTraderInventory(_trader, false);
        int itemNumber = ConsoleUtilities.InputInteger("Which item do you want to examine (enter the item #)?", minValue: 1, maxValue: _trader.Inventory.Count);
        int itemIndex = itemNumber - 1;
        Item item = _trader.Inventory[itemIndex];
        Console.WriteLine($"You examine the {item.Name}. (#{itemNumber})");
        item.Examine();
        Console.WriteLine(item.StatsFull);
    }
}