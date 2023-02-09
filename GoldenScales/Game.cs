//(c) copyright by Martin M. Klöckener
using System.Security.Cryptography;
using System.Text;
using ConsoleAdventure.Items;
using ConsoleAdventure.Items.Armors;
using ConsoleAdventure.Items.Weapons;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Game
{
    public bool GameHasEnded;
    public static readonly List<Command> GlobalCommands = new();

    public static StateMachine StateMachine = new StateMachine();
    public static Explore ExplorationState;

    public static Game Singleton;
    private Dungeon dungeon;

    public string Seed;

    private bool _restart;

    public int TotalDiscoveredRooms
    {
        get
        {
            int discoveredRooms = 0;
            foreach (Room room in dungeon.Rooms)
            {
                if (room.Discovered) discoveredRooms++;
            }

            return discoveredRooms;
        }
    }

    public Game(string seed)
    {
        //singleton
        Singleton = this;
        
        //convert seed
        int intSeed = ConvertSeedToInt(seed);
        
        //set seed
        RandomUtilities.SetSeed(intSeed);
        Seed = seed;
        Console.WriteLine($"Seed is: {Seed}, intSeed: {intSeed}");

        //generate dungeon
        dungeon = DungeonGenerator.Generate( 25, 15, intSeed);
        
        //create player
        var player = new Player("Martin", 100, dungeon.StartingRoom);
        
        //print welcome text
        Console.WriteLine("Welcome to the musty dungeon! Legends say that a valuable treasure is hidden within these walls, guarded by a cruel Lindworm. \nWill you be able to defy all the dangers that lurk down here and live out the rest of your life in prosperity? \nGood luck, adventurer!\n");
        
        //create dungeon navigation
        var dungeonNavigation = new DungeonNavigation(dungeon, player);
        
        //Global Commands (game commands can be called from any game state)
        GlobalCommands.Clear();
        SetGlobalCommands();
        
        //cheat commands
        SetCheatCommands(dungeon);

        //Set up state machine
        StateMachine = new StateMachine();
        ExplorationState = new Explore(dungeon, player, dungeonNavigation);

        StateMachine.SetState(ExplorationState);

        //Start Game Loop
        GameLoop();
    }

    private void GameLoop()
    {
        while (!GameHasEnded)
        {
            StateMachine.Tick();
        }

        if (_restart) return;
        bool startNewGame = ConsoleUtilities.InputBoolean("\nWould you like to try your luck again and descend into the dark depths?");
        if(!startNewGame) Environment.Exit(0);
    }
    
    private static int ConvertSeedToInt(string userInput)
    {
        bool userEnteredAnInteger = int.TryParse(userInput, out int userInputAsInt);
        var algo = SHA1.Create();
        int seed = userEnteredAnInteger ? userInputAsInt : BitConverter.ToInt32(algo.ComputeHash(Encoding.UTF8.GetBytes(userInput)));

        return seed;
    }

    private void ListAvailableCommands() => CommandUtilities.ListAvailableCommands(StateMachine.CurrentState.AvailableCommands);

    private void SetGlobalCommands()
    {
        var gameCommands = new List<Command>();

        var helpCommand = new Command("options (list all available commands)", new List<string> { "help", "options", "commands", "actions" }, ListAvailableCommands, true);
        var quitCommand = new Command("quit game (quit the game)", new List<string> { "quit game" }, Quit);
        var restartCommand = new Command("restart (start a new game)", new List<string> { "restart" }, Restart);
        var showHealthCommand = new Command("health (show your current health)", new List<string> { "health" }, () => Console.WriteLine($"Your current health is {Player.Singleton.Health}."));
        var seedCommand = new Command("seed (show the seed of the current dungeon layout)", new List<string> { "seed" }, () => Console.WriteLine($"The seed for this dungeon layout is: \"{Seed}\". Enter it at the beginning of a new game to recreate the same layout of rooms (loot and enemies vary)."));

        gameCommands.Add(helpCommand);
        gameCommands.Add(quitCommand);
        gameCommands.Add(restartCommand);
        gameCommands.Add(showHealthCommand);
        gameCommands.Add(seedCommand);
        
        GlobalCommands.AddRange(gameCommands);
    }

    private void SetCheatCommands(Dungeon dungeon)
    {
        var cheatCommands = new List<Command>();
        
        var mapFull = new Command("marco polo (show the full map, not just the dicovered area)", new List<string> {"marco polo"}, () => Map.Draw(dungeon, Player.Singleton, false), true);
        var richKid = new Command("rich kid (add a loooot of money to your inventory)", new List<string> { "i am a rich kid" }, () => Player.Singleton.AddGold(1000000), true);
        
        cheatCommands.Add(mapFull);
        cheatCommands.Add(richKid);
        
        GlobalCommands.AddRange(cheatCommands);
    }

    private static void Quit()
    {
        bool quit = ConsoleUtilities.InputBoolean("Do you really want to quit the game? All progress will be lost!");
        if(quit) Environment.Exit(0);
    }

    private void Restart()
    {
        bool restart = ConsoleUtilities.InputBoolean("Do you really want to start a new game? All progress will be lost!");
        if (restart)
        {
            GameHasEnded = true;
            _restart = true;
        }
    }
}