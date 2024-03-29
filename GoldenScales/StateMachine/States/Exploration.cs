﻿//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Explore : IState
{
    public List<Command> AvailableCommands { get; set; }
    public string TextWhenReturningToThisState => "You continue exploring the dungeon.";
    
    private readonly Dungeon Dungeon;
    private readonly Player Player;
    private readonly DungeonNavigation DungeonNavigation;

    public Explore(Dungeon dungeon, Player player, DungeonNavigation dungeonNavigation)
    {
        AvailableCommands = new List<Command>();
        Dungeon = dungeon;
        Player = player;
        DungeonNavigation = dungeonNavigation;
    }


    public void OnEnter()
    {
        //update available commands
        UpdateAvailableCommands();
    }

    public void Tick()
    {
        UpdateAvailableCommands();
        string userInput = ConsoleUtilities.InputString("What do you want to do next?");
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
        AddExploreCommands();
        AddRoomCommands();
    }

    private void AddExploreCommands()
    {
        var travelCommand = new Command("go + direction (e.g. \"go north\")", new List<string> { "go", "travel" }, DungeonNavigation.Go);
        var mapCommand = new Command("map (look at your map)", new List<string> { "map" }, () => Map.Draw(Dungeon, Player, true));
        var listDoorsCommand = new Command("doors (what doors are in this room)", new List<string> { "doors", "where" }, () => CommandUtilities.ListAvailableDoors(Player.CurrentRoom));
        var openInventoryCommand = new Command("inventory (open your inventory to look at your items)", new List<string>{"open inventory", "inventory"}, () => Player.Singleton.OpenInventory());

        AvailableCommands.Add(travelCommand);
        AvailableCommands.Add(mapCommand);
        AvailableCommands.Add(listDoorsCommand);
        AvailableCommands.Add(openInventoryCommand);
    }

    private void AddRoomCommands()
    {
        AvailableCommands.AddRange(Player.CurrentRoom.RoomCommands);
    }
}