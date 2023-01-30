//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class SearchInventory : IState
{
    public List<Command> AvailableCommands { get; set; }
    public IState PreviousState;
    public string TextWhenReturningToThisState => "You're back in your inventory.";

    public SearchInventory(IState previousState)
    {
        PreviousState = previousState;
        AvailableCommands = new List<Command>();
    }


    public void OnEnter()
    {
        //update available commands
        UpdateAvailableCommands();
        ListItems();
    }

    public void Tick()
    {
        UpdateAvailableCommands();
        string userInput = ConsoleUtilities.InputString("What do you want to do in your inventory?");
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

        var closeInventoryCommand = new Command("close (close your inventory)", new List<string> { "close", "back" }, () => Player.Singleton.CloseInventory());
        var listItemsCommand = new Command("list items (lists all items in your invenory)", new List<string> { "list items", "list inventory" }, ListItems);
        var examineCommand = new Command("examine + item # (e.g. \"examine 2\" Examine an item and learn more about its stats)", new List<string> { "examine" }, ExamineItem);
        var equipCommand = new Command("equip + item # (e.g. \"equip 2\" Equip an item)", new List<string> { "equip" }, EquipItem);
        var consumeCommand = new Command("use/eat/drink  + item # (consume an item that's in your inventory, e.g. \"drink 2\")", new List<string> { "consume", "drink", "eat", "use" }, ConsumeItem);
        var statsCommand = new Command("stats + item # (show the detailed stats of an item)", new List<string> { "stats" }, DisplayItemStats);

        AvailableCommands.Add(closeInventoryCommand);
        AvailableCommands.Add(listItemsCommand);
        AvailableCommands.Add(examineCommand);
        AvailableCommands.Add(equipCommand);
        AvailableCommands.Add(consumeCommand);
        AvailableCommands.Add(statsCommand);
    }

    private void ListItems()
    {
        if (Player.Singleton.Inventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty.");
            return;
        }
        
        Console.WriteLine("You have the folling items in your inventory: ");

        for (int i = 0; i < Player.Singleton.Inventory.Count; i++)
        {
            int itemNumber = i + 1;
            Item item = Player.Singleton.Inventory[i];

            string itemString = $"#{itemNumber}: {item.Name} ({item.StatsShort})";
            Console.WriteLine(itemString);
        }
    }

    private void ExamineItem(List<string> userParameters)
    {
        //try to get the item that the player wants to examine
        Item item = StringParser.InventoryItemFromString(userParameters);
        if (item == null)
        {
            Console.WriteLine("Which item do you want to examine? Please type in the form of \"examine 2\", where 2 is the item number in the inventory (type \"list items\" to list all your items).");
            return;
        }
        
        //if we managed to get the item, examine it
        Console.WriteLine($"You examine your {item.Name} (#{Player.Singleton.Inventory.IndexOf(item) + 1}).");
        item.Examine();
    }

    private void EquipItem(List<string> userParameters)
    {
        //try to get the item that the player wants to equip
        Item item = StringParser.InventoryItemFromString(userParameters);
        if (item == null)
        {
            Console.WriteLine("Which item do you want to equip? Please type in the form of \"equip 2\", where 2 is the item number in the inventory (type \"list items\" to list all your items).");
            return;
        }

        //if we managed to get the item, check if it's equippable
        int itemNumber = Player.Singleton.Inventory.IndexOf(item) + 1;
        
        if (!item.Equippable)
        {
            Console.WriteLine($"You can't equip the item {item.Name} (#{itemNumber.ToString()}).");
            return;
        }

        //if it's equippable, equip it (and unequip potentially equipped item in the same slot)
        if (Player.Singleton.Equip(item as Equippable))
        {
            Console.WriteLine($"You equip your {item.Name} (#{itemNumber.ToString()}) at the {(item as Equippable).EquipSlot.ToString()} slot.");
        }
    }

    private void UnEquipItem(Item item)
    {
        
    }

    private void ConsumeItem(List<string> userParameters)
    {
        //try to get the item that the player wants to consume
        string verb = "use";
        if(userParameters.Contains("drink")) verb = "drink";
        else if (userParameters.Contains("eat")) verb = "eat";
        
        Item item = StringParser.InventoryItemFromString(userParameters);
        if (item == null)
        {
            Console.WriteLine($"Which item do you want to {verb}? Please type in the form of \"{verb} 2\", where 2 is the item number in the inventory (type \"list items\" to list all your items).");
            return;
        }
        
        //if we managed to get the item, check if it's consumable
        int itemNumber = Player.Singleton.Inventory.IndexOf(item) + 1;
        
        if (!item.Consumable)
        {
            Console.WriteLine($"You can't {verb} the item {item.Name} (#{itemNumber.ToString()}).");
            return;
        }

        //if it's consumable, consume and remove it from the inventory
        (item as Consumable).Consume();
        if (!Player.Singleton.Inventory.Contains(item))
        {
            Console.WriteLine("Huh, you managed to consume an item that's not in your inventory. How's that possible?");
            return;
        }
        
        Player.Singleton.Inventory.Remove(item);
    }

    private void DisplayItemStats(List<string> userParameters)
    {
        //try to get the item that the player wants to show stats of
        Item item = StringParser.InventoryItemFromString(userParameters);
        if (item == null)
        {
            Console.WriteLine("Which item do you want to see the stats of? Please type in the form of \"stats 2\", where 2 is the item number in the inventory (type \"list items\" to list all your items).");
            return;
        }
        
        //if we managed to get the item, show stats
        Console.WriteLine($"Stats of item #{Player.Singleton.Inventory.IndexOf(item) + 1}:");
        Console.WriteLine(item.StatsFull);
    }
}