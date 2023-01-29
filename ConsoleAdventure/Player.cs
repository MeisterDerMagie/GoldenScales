//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Items;
using ConsoleAdventure.Utilities;

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

    public int Gold { get; private set; }
    
    public List<Item> Inventory = new();
    public Dictionary<EquipSlot, Equippable> EquippedItems;

    public static Player Singleton;

    #region Gold
    public void AddGold(int amount, bool silently = false)
    {
        Gold += amount;
        if(!silently) Console.WriteLine($"You add {amount} gold to your purse and now have a total of {Gold}.");
    }

    public bool HasGold(int amount)
    {
        return (Gold >= amount);
    }

    public void RemoveGold(int amount)
    {
        Gold -= amount;
        Console.WriteLine("Your purse is now ");
    }
    #endregion

    #region Inventory
    public void AddToIventory(Item item, bool silently = false)
    {
        Inventory.Add(item);
        if(!silently) Console.WriteLine($"You add {item.Name} to your inventory.");
    }

    public void OpenInventory()
    {
        var inventoryState = new SearchInventory(Game.StateMachine.CurrentState);
        Game.StateMachine.SetState(inventoryState);
    }

    public void CloseInventory()
    {
        if (Game.StateMachine.CurrentState is not SearchInventory)
        {
            Console.WriteLine("ERROR: Can't close the inventory, because apparently it's not open.");
            return;
        }

        //restore previous state
        var inventoryState = Game.StateMachine.CurrentState as SearchInventory;
        Console.WriteLine("You close your inventory.");
        Game.StateMachine.SetState(inventoryState.PreviousState, false);
    }
    #endregion
    
    #region Equipment
    public bool Equip(Equippable item)
    {
        if (item == null)
        {
            Console.WriteLine("ERROR: Item to equip can't be null.");
            return false;
        }
        
        if (!item.Equippable)
        {
            Console.WriteLine($"You can't equip the item {item.Name}.");
            return false;
        }
        
        bool slotIsOccupied = EquippedItems[item.EquipSlot] != null;

        if (!slotIsOccupied)
        {
            EquippedItems[item.EquipSlot] = item;
            return true;
        }
        else
        {
            bool playerWantsToUnequip = ConsoleUtilities.InputBoolean($"There already is an item equipped at the {item.EquipSlot.ToString()} slot. Do you want to unequip it and equip the new item instead?");
            if (!playerWantsToUnequip)
            {
                Console.WriteLine("You decided not to equip the item.");
                return false;
            }
            
            //unequip
            AddToIventory(EquippedItems[item.EquipSlot], true);
            
            //then equip new item
            EquippedItems[item.EquipSlot] = item;
            return true;
        }
    }

    public bool UnEquip(Item item, bool silently = false)
    {
        throw new NotImplementedException();
    }

    public void UnEquip(EquipSlot slot, bool silently = false)
    {
        if (EquippedItems[slot] == null)
        {
            if(!silently)
                Console.WriteLine($"Can't unequip the {slot.ToString()} slot because there is no item equipped.");
            return;
        }

        Item equippedItem = EquippedItems[slot];
        
        AddToIventory(equippedItem, true);
        EquippedItems[slot] = null;
        
        if (!silently)
            Console.WriteLine($"You unequipped the {equippedItem.Name}.");
    }
    #endregion
    
    public Player(string name, int maxHealth, Room startingRoom)
    {
        CurrentRoom = startingRoom;
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
        
        //set up equippedItems dictionary
        EquippedItems = new Dictionary<EquipSlot, Equippable>();
        foreach (EquipSlot equipSlot in Enum.GetValues<EquipSlot>())
        {
            EquippedItems.Add(equipSlot, null);
        }
        
        //pseudo singleton
        Singleton = this;
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
        if (Health == MaxHealth)
        {
            Console.WriteLine("You heal all your wounds, but ... there were no wounds, you already had full health. That was wasted!");
            return;
        }
        
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