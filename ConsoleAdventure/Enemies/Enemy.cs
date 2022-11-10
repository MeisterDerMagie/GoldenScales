//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Enemies;

public abstract class Enemy : IDamageable
{
    public string Name { get; private set; }
    
    public int MaxHealth { get; private set; }
    public int Health { get; private set; }

    public event Action OnEnemyDied;

    public Enemy(string _name, int _maxHealth)
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

    protected virtual void Die()
    {
        OnEnemyDied?.Invoke();
    }
}