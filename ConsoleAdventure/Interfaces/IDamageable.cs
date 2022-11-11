//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public interface IDamageable
{
    public int MaxHealth { get; }
    public int Health { get; }

    public void DealDamage(int amount);
    public void Heal(int amount);
}