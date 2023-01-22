using System;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;
internal static class Program
{
    private static void Main(string[] args)
    {
        //seed
        int randomSeed = new Random().Next(int.MaxValue);
        RandomUtilities.SetSeed(randomSeed);
        Console.WriteLine($"Seed is: {randomSeed}");
        
        //generate dungeon
        var dungeon = DungeonGenerator.Generate( 25, 15, randomSeed);
        var player = new Player("Martin", 100);
        
        Map.Draw(dungeon, player);


        //Welcome text
        Console.WriteLine("Welcome to the musty dungeon! Legends say that a valuable treasure is hidden within these walls, guarded by a cruel Lindworm. \nWill you be able to defy all the dangers that lurk down here and live out the rest of your life in prosperity? \nGood luck!");
    }
}