//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Items;

namespace ConsoleAdventure;

public static class Score
{
    public static int CalculateScore(bool defeatedBoss)
    {
        int score = 0;

        //total gold value of items in inventory
        foreach (Item item in Player.Singleton.Inventory)
        {
            score += item.GoldValue;
        }
        
        //remaining hp
        score += Player.Singleton.Health;
        
        //bonus score if the endboss has been defeated
        if(defeatedBoss) score += 250;
        
        //return
        return score;
    }
}