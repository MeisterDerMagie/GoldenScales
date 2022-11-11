//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Enemies;

public abstract class Enemy : IDamageable
{
    public string Name { get; private set; }
    
    public int MaxHealth { get; private set; }
    public int Health { get; private set; }

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
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
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