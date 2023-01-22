//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Enemies;

public abstract class Enemy : IDamageable
{
    public string Name { get; private set; }
    
    public int MaxHealth { get; private set; }
    public int Health
    {
        get => _health;
        private set
        {
            if (value < 0) value = 0;
            else if (value > MaxHealth) value = MaxHealth;
            _health = value;
        }
    }

    private int _health;

    public event Action OnEnemyDied;

    public Enemy(string name, int maxHealth)
    {
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
        if(Health + amount >= MaxHealth)
            HealFully();
        else
        {
            Health += amount;
            Console.WriteLine($"The {Name} heals for {amount} and is back to {Health} hitpoints.");
        }
    }

    public void HealFully()
    {
        Health = MaxHealth;
        Console.WriteLine($"The {Name} heals all their wounds and is back to full strength. Oh oh, that's not good for you!");
    }

    private void CheckForDeath()
    {
        if(Health <= 0) Die();
    }

    protected virtual void Die()
    {
        OnEnemyDied?.Invoke();
    }
}