using System;
using System.Security.Cryptography;
using System.Text;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;
internal static class Program
{
    private static void Main(string[] args)
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("A new adventure begins.");

            int seed = GetSeed();

            var game = new Game(seed);
        }
    }

    private static int GetSeed()
    {
        bool wantsToEnterOwnSeed = ConsoleUtilities.InputBoolean("Do you want to input a custom seed?");
        int seed;
            
        //if the player wants to enter a custom seed
        if (wantsToEnterOwnSeed)
        {
            string userInput = ConsoleUtilities.InputString("Enter a seed for the procedural generation of the dungeon: ");
            var algo = SHA1.Create();
            seed = BitConverter.ToInt32(algo.ComputeHash(Encoding.UTF8.GetBytes(userInput)));
        }

        //otherwise generate a random seed
        else
        {
            seed = new Random().Next(int.MinValue, int.MaxValue);
            RandomUtilities.SetSeed(seed);
        }
            
        Console.WriteLine($"Seed is: {seed}");

        return seed;
    }
}