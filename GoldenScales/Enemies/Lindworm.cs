//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;
using ConsoleAdventure.Items.EnemyWeapons;

namespace ConsoleAdventure.Enemies;

public class Lindworm : Enemy
{
    public Lindworm() : base("Lindworm", Constants.LindwormMaxHealth, new LindwormBite()) { }
    
    protected override void Die()
    {
        base.Die();
        Console.WriteLine("The lindworm roars loudly one last time and then falls onto the huge treasure. Gold coins jingle, the fading light of the lindworm's eyes still reflected in the thousand shiny surfaces. Then there is silence. \nYou have done it. You have defeated the lindworm. The treasure is now yours! As you take the first valuables and dream of a bright future, you suddenly feel strange. Your clothes feel tight. Your perception suddenly changes. You notice the smallest movements in the room. Woodlice crawl across the floor. A fly at the other end of the room. You smell every rat, every bat. Nothing escapes your attention. \nThen you look down at yourself and see your skin changing. You grow golden scales! The treasure is cursed, you yourself become the new Lindworm! An irrepressible greed and a desire for riches flares up in you. A rage like you have never felt before. \nThe last clear thought you can think is: \"I wanted to live the rest of my life in wealth, and I do now. But I had imagined it much differently...\" After that, all you feel is greed. \nIt will only be a matter of time before a new adventurer finds his way here and falls to the same fate as you.");

        Game.Singleton.GameHasEnded = true;
        
        //show score
        Console.WriteLine("The adventure is over. You managed to defeat the Lindworm and took its place. Congratulations!");
        Console.WriteLine($"Your final score is: {Score.CalculateScore(true)}");
        Console.WriteLine($"The seed for this dungeon layout was \"{Game.Singleton.Seed}\". Enter this at the beginning of a game to recreate the same dungeon (loot varies).");
    }
}