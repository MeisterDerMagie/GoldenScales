//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public class Player : IDamageable
{
    public string Name { get; private set; }
    
    public int MaxHealth { get; private set; }
    public int Health { get; private set; }


    public Player(string _name, int _maxHealth)
    {
        Name = _name;
        MaxHealth = _maxHealth;
        Health = _maxHealth;
    }
    
    public void DealDamage(int _amount)
    {
        Health -= _amount;
        CheckForDeath();
    }

    public void Heal(int _amount)
    {
        Health += _amount;
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