//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Items;

namespace ConsoleAdventure;

public class Player : IDamageable
{
    public Room CurrentRoom;
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

    public List<Item> Inventory = new();


    public Player(string name, int maxHealth, Room startingRoom)
    {
        CurrentRoom = startingRoom;
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }
    
    public void DealDamage(int amount)
    {
        Health -= amount;
        Console.WriteLine($"You receive {amount} damage and now have {Health} health.");
        CheckForDeath();
    }

    public void Heal(int amount)
    {
        if(Health + amount >= MaxHealth)
            HealFully();
        else
        {
            Health += amount;
            Console.WriteLine($"You healed your wounds for {amount} health and are now back to a total of {Health}. A slight relief!");
        }
    }

    public void HealFully()
    {
        Health = MaxHealth;
        Console.WriteLine($"All your wounds heal and you are back to your full health ({MaxHealth}). That felt great!");
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