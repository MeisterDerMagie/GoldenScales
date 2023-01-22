//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Enemies;

public class Spider : Enemy
{
    public Spider() : base("Spider", Constants.SpiderMaxHealth) { }
    
    protected override void Die()
    {
        Console.WriteLine("The spider wriggles its eight legs one last time, then breathes its last.");
        base.Die();
    }
}