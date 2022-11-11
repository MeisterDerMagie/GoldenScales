//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Enemies;

public class Skeleton : Enemy
{
    public Skeleton() : base("Skeleton", Constants.SkeletonMaxHealth) { }
    
    protected override void Die()
    {
        Console.WriteLine("The skeleton rattles one last time and collapses into a pile of bones.");
        base.Die();
    }
}