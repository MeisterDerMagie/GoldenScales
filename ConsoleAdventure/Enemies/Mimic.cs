//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Enemies;

public class Mimic : Enemy
{
    public Mimic() : base("Mimic", Constants.MimicMaxHealth) { }
    
    protected override void Die()
    {
        Console.WriteLine("The Mimic bites one last time - but you can dodge it. Then she drools and the lid slams shut. Inside you can find nothing but sinking goo. Ugh!");
        base.Die();
    }
}