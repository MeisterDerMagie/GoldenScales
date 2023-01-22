using System;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;
internal static class Program
{
    private static void Main(string[] args)
    {
        //random seed
        
        int seed = new Random().Next(int.MinValue, int.MaxValue);
        RandomUtilities.SetSeed(seed);
        Console.WriteLine($"Seed is: {seed}");
        
        
        
        //good seeds:
        //2070994485
        
        //fixed seed
        /*
        int seed = 12345;
        RandomUtilities.SetSeed(seed);
        Console.WriteLine($"Seed is: {seed}");
        */
        
        
        //generate dungeon
        Dungeon dungeon = DungeonGenerator.Generate( 25, 15, seed);
        var player = new Player("Martin", 100, dungeon.StartingRoom);
        
        //draw dungeon map
        Map.Draw(dungeon, player, false);
        
        //Welcome text
        Console.WriteLine("Welcome to the musty dungeon! Legends say that a valuable treasure is hidden within these walls, guarded by a cruel Lindworm. \nWill you be able to defy all the dangers that lurk down here and live out the rest of your life in prosperity? \nGood luck, adventurer!");

        
        //create dungeon navigation
        var dungeonNavigation = new DungeonNavigation(dungeon, player);
        
        //TEST: Dungeon navigation
        dungeonNavigation.Travel(Direction.North);
    }
}