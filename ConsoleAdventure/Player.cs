//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public class Player : IDamageable
{
    public RoomPosition CurrentPosition;
    public string Name { get; private set; }
    
    public int MaxHealth { get; private set; }
    public int Health { get; private set; }


    public Player(string name, int maxHealth)
    {
        CurrentPosition = new RoomPosition(0, 0);
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }
    
    public void DealDamage(int amount)
    {
        Health -= amount;
        CheckForDeath();
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
    }

    private void CheckForDeath()
    {
        if(Health <= 0) Die();
    }

    private void Die()
    {
        Console.WriteLine("You died. The adventure is over.");
    }
}