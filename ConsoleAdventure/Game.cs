//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Game
{
    public bool GameHasEnded;
    private readonly List<Command> _availableCommands = new();

    public Game(int seed)
    {
        //generate dungeon
        Dungeon dungeon = DungeonGenerator.Generate( 25, 15, seed);
        
        //create player
        var player = new Player("Martin", 100, dungeon.StartingRoom);
        
        //print welcome text
        Console.WriteLine("Welcome to the musty dungeon! Legends say that a valuable treasure is hidden within these walls, guarded by a cruel Lindworm. \nWill you be able to defy all the dangers that lurk down here and live out the rest of your life in prosperity? \nGood luck, adventurer!\n");
        
        //create dungeon navigation
        var dungeonNavigation = new DungeonNavigation(dungeon, player);
        
        //Game Commands (game commands can be called from any game state)
        var gameCommands = new List<Command>();

        var helpCommand = new Command("options (list all available commands)", new List<string>() { "help", "options", "commands", "actions", "list" }, ListAvailableCommands);
        gameCommands.Add(helpCommand);
        var quitCommand = new Command("quit (quit the game)", new List<string>() { "quit", "q", "exit" }, Quit);
        gameCommands.Add(quitCommand);
        var restartCommand = new Command("restart (start a new game)", new List<string>() { "restart" }, Restart);
        gameCommands.Add(restartCommand);
        
        //cheat commands
        var cheatCommands = new List<Command>();
        var mapFull = new Command("mapFull (show the full map, not just the dicovered area)", new List<string>(){"mapFull"}, () => Map.Draw(dungeon, player, false), true);
        cheatCommands.Add(mapFull);
        
        //travel command
        var travelCommand = new Command("go + direction (e.g. \"go north\")", new List<string>() { "go", "travel" }, dungeonNavigation.Go);
        var mapCommand = new Command("map (look at your map)", new List<string>() { "map" }, () => Map.Draw(dungeon, player, true));
        var listDoorsCommand = new Command("doors (what doors are in this room)", new List<string>() { "doors", "where" }, () => CommandUtilities.ListAvailableDoors(player.CurrentRoom));

        //available commands
        _availableCommands.AddRange(gameCommands);
        _availableCommands.AddRange(cheatCommands);
        _availableCommands.Add(travelCommand);
        _availableCommands.Add(mapCommand);
        _availableCommands.Add(listDoorsCommand);
        
        //Start Game Loop
        GameLoop();
    }

    private void GameLoop()
    {
        while (!GameHasEnded)
        {
            string userInput = ConsoleUtilities.InputString("What do you want to do next?");
            if (CommandUtilities.TryExecuteUserInput(userInput, _availableCommands) == false)
            {
                Console.WriteLine("Type \"options\" to see a list of all actions you can do right now.");
            }
        }
    }

    private void ListAvailableCommands() => CommandUtilities.ListAvailableCommands(_availableCommands);

    private static void Quit()
    {
        bool quit = ConsoleUtilities.InputBoolean("Do you really want to quit? All progress will be lost!");
        if(quit) Environment.Exit(0);
    }

    private void Restart()
    {
        bool restart = ConsoleUtilities.InputBoolean("Do you really want to start a new game? All progress will be lost!");
        if (restart) GameHasEnded = true;
    }
}