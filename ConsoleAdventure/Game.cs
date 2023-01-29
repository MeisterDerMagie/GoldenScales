//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Game
{
    public bool GameHasEnded;
    public static readonly List<Command> GlobalCommands = new();

    public static StateMachine StateMachine = new StateMachine();
    public static Explore ExplorationState;

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
        
        //Global Commands (game commands can be called from any game state)
        GlobalCommands.Clear();
        SetGlobalCommands();
        
        //cheat commands
        SetCheatCommands(dungeon);

        //Set up state machine
        StateMachine = new StateMachine();
        ExplorationState = new Explore(dungeon, player, dungeonNavigation);

        StateMachine.SetState(ExplorationState);

        //add items to inventory
        Player.Singleton.AddToIventory(ItemUtilities.GenerateRandomItem(12));
        Player.Singleton.AddToIventory(ItemUtilities.GenerateRandomItem(45));
        Player.Singleton.AddToIventory(ItemUtilities.GenerateRandomItem(28));
        Player.Singleton.AddToIventory(ItemUtilities.GenerateRandomItem(28));
        Player.Singleton.AddToIventory(ItemUtilities.GenerateRandomItem(28));
        Player.Singleton.AddToIventory(ItemUtilities.GenerateRandomItem(28));
        
        //Start Game Loop
        GameLoop();
    }

    private void GameLoop()
    {
        while (!GameHasEnded)
        {
            StateMachine.Tick();
        }
    }

    private void ListAvailableCommands() => CommandUtilities.ListAvailableCommands(StateMachine.CurrentState.AvailableCommands);

    private void SetGlobalCommands()
    {
        var gameCommands = new List<Command>();

        var helpCommand = new Command("options (list all available commands)", new List<string> { "help", "options", "commands", "actions" }, ListAvailableCommands, true);
        var quitCommand = new Command("quit (quit the game)", new List<string> { "quit", "q", "exit" }, Quit);
        var restartCommand = new Command("restart (start a new game)", new List<string> { "restart" }, Restart);
        var showHealthCommand = new Command("health (show your current health)", new List<string> { "health", "hp" }, () => Console.WriteLine($"Your current health is {Player.Singleton.Health}."));

        gameCommands.Add(helpCommand);
        gameCommands.Add(quitCommand);
        gameCommands.Add(restartCommand);
        gameCommands.Add(showHealthCommand);
        
        GlobalCommands.AddRange(gameCommands);
    }

    private void SetCheatCommands(Dungeon dungeon)
    {
        var cheatCommands = new List<Command>();
        
        var mapFull = new Command("mapFull (show the full map, not just the dicovered area)", new List<string> {"mapFull"}, () => Map.Draw(dungeon, Player.Singleton, false), true);
        
        cheatCommands.Add(mapFull);
        
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
        if (restart) GameHasEnded = true;
    }
}