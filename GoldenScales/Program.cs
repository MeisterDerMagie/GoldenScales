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

            //seed
            string seed;
            bool wantsToEnterOwnSeed = ConsoleUtilities.InputBoolean("Do you want to input a custom seed?");
            //if the player wants to enter a custom seed
            if(wantsToEnterOwnSeed)
                seed = ConsoleUtilities.InputString("Enter a seed for the procedural generation of the dungeon: ");
            //if the player doesn't want to enter a custom seed, generate a random one
            else
                seed = new Random().Next(int.MinValue, int.MaxValue).ToString();

            //start game
            var game = new Game(seed);
        }
    }
}