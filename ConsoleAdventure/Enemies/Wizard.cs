﻿//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Enemies;

public class Wizard : Enemy
{
    public Wizard() : base("Wizard", Constants.WIZARD_MAX_HEALTH) { }
    
    protected override void Die()
    {
        Console.WriteLine("The wizard takes his last magical breath and then falls lifeless to the ground.");
        base.Die();
    }
}