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
        InventoryUtilities.PrintPlayerInventory(Player.Singleton);
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
        var listItemsCommand = new Command("list items (lists all items in your invenory)", new List<string> { "list items", "list inventory" }, () => InventoryUtilities.PrintPlayerInventory(Player.Singleton));
        var examineCommand = new Command("examine + item # (Examine an item and learn more about its stats, e.g. \"examine 2\")", new List<string> { "examine" }, ExamineItem);
        var equipCommand = new Command("equip + item # (Equip an item, e.g. \"equip 2\")", new List<string> { "equip" }, EquipItem);
        var unequipCommand = new Command("unequip + item # (Unequip an item, e.g. \"unequip 2\")", new List<string>{"unequip"}, UnEquipItem);
        var consumeCommand = new Command("use/eat/drink  + item # (consume an item that's in your inventory, e.g. \"drink 2\")", new List<string> { "consume", "drink", "eat", "use" }, ConsumeItem);
        var statsCommand = new Command("stats + item # (show the detailed stats of an item)", new List<string> { "stats" }, DisplayItemStats);
        var totalArmorCommand = new Command("total armor (show your total armor value)", new List<string> { "total armor" }, () => Console.WriteLine($"You currently have a total armor value of {Player.Singleton.TotalArmorProtection}."));
        var totalGoldValueCommand = new Command("total gold value (show the total gold value of all your items)", new List<string>{"total gold value"}, () => Console.WriteLine($"The total gold value of all your items is: {InventoryUtilities.CalculateTotalGoldValueOfAllInventoryItems(Player.Singleton.Inventory)}"));
        var sortInventoryCommand = new Command("sort (sort your inventory by item category and price)", new List<string> { "sort" }, () => { InventoryUtilities.SortInventory(Player.Singleton.Inventory); InventoryUtilities.PrintPlayerInventory(Player.Singleton); });

        AvailableCommands.Add(closeInventoryCommand);
        AvailableCommands.Add(listItemsCommand);
        AvailableCommands.Add(examineCommand);
        AvailableCommands.Add(equipCommand);
        AvailableCommands.Add(unequipCommand);
        AvailableCommands.Add(consumeCommand);
        AvailableCommands.Add(statsCommand);
        AvailableCommands.Add(totalArmorCommand);
        AvailableCommands.Add(totalGoldValueCommand);
        AvailableCommands.Add(sortInventoryCommand);
    }

    private void ExamineItem(List<string> userParameters)
    {
        //try to get the item that the player wants to examine
        (Item item, int itemNumber) = StringParser.InventoryItemFromString(userParameters, Player.Singleton.Inventory);
        
        if (item == null)
        {
            Console.WriteLine("Which item do you want to examine? Please type in the form of \"examine 2\", where 2 is the item number in the inventory (type \"list items\" to list all your items).");
            return;
        }
        
        //if we managed to get the item, examine it
        Console.WriteLine($"You examine your {item.Name} (#{itemNumber}).");
        item.Examine();
    }

    private void EquipItem(List<string> userParameters)
    {
        //try to get the item that the player wants to equip
        (Item item, int itemNumber) = StringParser.InventoryItemFromString(userParameters, Player.Singleton.Inventory);

        if (item == null)
        {
            Console.WriteLine("Which item do you want to equip? Please type in the form of \"equip 2\", where 2 is the item number in the inventory (type \"list items\" to list all your items).");
            return;
        }

        //if we managed to get the item, check if it's equippable
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

    private void UnEquipItem(List<string> userParameters)
    {
        //try to get the item that the player wants to unequip
        (Item item, int itemNumber) = StringParser.InventoryItemFromString(userParameters, Player.Singleton.Inventory);
        
        if (item == null)
        {
            Console.WriteLine("Which item do you want to unequip? Please type in the form of \"unequip 2\", where 2 is the item number in the inventory (type \"list items\" to list all your items).");
            return;
        }
        
        //if we managed to get the item, check if it's equippable and equipped
        if (item is not Equippable equippable || !equippable.IsEquipped)
        {
            Console.WriteLine($"You can't unequip the item {item.Name} (#{itemNumber.ToString()}) because it's not equipped.");
            return;
        }
        
        //if it's equippable and equipped, unequip it
        if (Player.Singleton.UnEquip(item))
        {
            Console.WriteLine($"You unequip your {item.Name} (#{itemNumber.ToString()}).");
        }
    }

    private void ConsumeItem(List<string> userParameters)
    {
        //try to get the item that the player wants to consume
        string verb = "use";
        if(userParameters.Contains("drink")) verb = "drink";
        else if (userParameters.Contains("eat")) verb = "eat";
        
        (Item item, int itemNumber) = StringParser.InventoryItemFromString(userParameters, Player.Singleton.Inventory);

        if (item == null)
        {
            Console.WriteLine($"Which item do you want to {verb}? Please type in the form of \"{verb} 2\", where 2 is the item number in the inventory (type \"list items\" to list all your items).");
            return;
        }
        
        //if we managed to get the item, check if it's consumable
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
        (Item item, int itemNumber) = StringParser.InventoryItemFromString(userParameters, Player.Singleton.Inventory);

        if (item == null)
        {
            Console.WriteLine("Which item do you want to see the stats of? Please type in the form of \"stats 2\", where 2 is the item number in the inventory (type \"list items\" to list all your items).");
            return;
        }
        
        //if we managed to get the item, show stats
        Console.WriteLine($"Stats of item #{itemNumber}:");
        Console.WriteLine(item.StatsFull);
    }
}