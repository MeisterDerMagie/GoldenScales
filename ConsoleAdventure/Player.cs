//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.DataTypes;
using ConsoleAdventure.Items;
using ConsoleAdventure.Items.Armors;
using ConsoleAdventure.Items.Consumables;
using ConsoleAdventure.Items.Weapons;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Player : IDamageable
{
    public Room CurrentRoom
    {
        get => _currentRoom;
        set
        {
            PreviousRoom = _currentRoom;
            _currentRoom = value;
        }
    }

    private Room _currentRoom;

    public Room PreviousRoom;
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
    
    public Weapon Weapon => EquippedItems[EquipSlot.Weapon] == null ? new Fists() : EquippedItems[EquipSlot.Weapon] as Weapon ;

    public int Gold { get; private set; }
    
    public List<Item> Inventory = new();
    public Dictionary<EquipSlot, Equippable> EquippedItems;
    public int TotalArmorProtection => CalculateTotalProtection();

    public static Player Singleton;

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
        
        //give starting equipment
        GiveStartingEquipment();
        
        //pseudo singleton
        Singleton = this;
    }
    
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

    public void RemoveGold(int amount, bool silently = false)
    {
        Gold -= amount;
        if(!silently) Console.WriteLine($"You have {Gold} left gold in your purse.");
    }

    public void PrintPurse()
    {
        Console.WriteLine($"You have {Gold} gold in your purse.");
    }
    #endregion

    #region Inventory
    public void AddToIventory(Item item, bool silently = false)
    {
        Inventory.Add(item);
        if(!silently) Console.WriteLine($"You add {item.Name} to your inventory.");
    }

    public bool RemoveFromInventory(Item item, bool silently = false)
    {
        if (!Inventory.Contains(item))
        {
            Console.WriteLine("ERROR: Can't remove item item from player inventory because the inventory doesn't contain it.");
            return false;
        }
        
        //unequip if equipped
        bool isEquipped = (item is Equippable equippable && equippable.IsEquipped);
        if (isEquipped) UnEquip(item, true);

        //remove
        Inventory.Remove(item);
        if (!silently)
        {
            string output = isEquipped ? $"You unequip {item.Name} and them remove it from your inventory." : $"You remove {item.Name} from your inventory." ;
            Console.WriteLine(output);
        }
        return true;
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

        if (!Inventory.Contains(item))
        {
            Console.WriteLine($"You can't equip {item.Name} because it's not in your inventory. Don't try to equip stuff you don't own, you little rascal!");
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
            if (EquippedItems[item.EquipSlot] == item)
            {
                Console.WriteLine($"{item.Name} is already equipped!");
                return false;
            }
            
            bool playerWantsToUnequip = ConsoleUtilities.InputBoolean($"There already is an item equipped at the {item.EquipSlot.ToString()} slot ({EquippedItems[item.EquipSlot].Name}). Do you want to unequip it and equip the new item ({item.Name}) instead?");
            if (!playerWantsToUnequip)
            {
                Console.WriteLine("You decide not to equip the item.");
                return false;
            }
            
            //equip new item
            EquippedItems[item.EquipSlot] = item;
            return true;
        }
    }

    public bool UnEquip(Item item, bool silently = false)
    {
        if (item is not Equippable equippable)
        {
            Console.WriteLine($"Can't unequip {item.Name} because it's not equippable.");
            return false;
        }

        if (!equippable.IsEquipped)
        {
            Console.WriteLine($"You can't unequip {item.Name} because it's not equipped.");
            return false;
        }

        return UnEquip(equippable.EquipSlot, silently);
    }

    public bool UnEquip(EquipSlot slot, bool silently = false)
    {
        if (EquippedItems[slot] == null)
        {
            if(!silently)
                Console.WriteLine($"Can't unequip the {slot.ToString()} slot because there is no item equipped.");
            return false;
        }

        Item equippedItem = EquippedItems[slot];
        
        EquippedItems[slot] = null;
        
        if (!silently)
            Console.WriteLine($"You unequipped the {equippedItem.Name}.");

        return true;
    }

    private int CalculateTotalProtection()
    {
        int totalProtection = 0;
        foreach (KeyValuePair<EquipSlot,Equippable> equippedItem in EquippedItems)
        {
            if (equippedItem.Value is not Armor armor) continue;
            totalProtection += armor.Protection;
        }

        return totalProtection;
    }

    private void GiveStartingEquipment()
    {
        var smallHealthPotion = new SmallHealthPotion();
        var mediumHealthPotion = new MediumHealthPotion();
        var knife = new Knife( new Range<int>(3, 3), new Range<int>(6, 6), new Range<float>(0.05f, 0.05f), new Range<float>(2f, 2f), new Range<float>(2f, 2f));
        var jacket = new Jacket(new Range<int>(2, 2));
        
        AddToIventory(smallHealthPotion, true);
        AddToIventory(mediumHealthPotion, true);
        AddToIventory(knife, true);
        AddToIventory(jacket, true);
        
        Equip(knife);
        Equip(jacket);
    }
    #endregion

    #region Health
    public void DealDamage(int amount)
    {
        if (amount == 0) return;
        
        //https://www.reddit.com/r/gamedesign/comments/oo099v/scaling_armor_values_in_a_leveled_dungeon_crawler/
        int finalAmount = amount * Constants.ArmorCoefficient / (Constants.ArmorCoefficient + TotalArmorProtection);
        Health -= finalAmount;

        if (TotalArmorProtection > 0)
            Console.WriteLine($"You receive {amount} damage but your armor can block {amount - finalAmount} ({100f/amount * (amount-finalAmount)}%) of it, so you only receive {finalAmount}. You now have {Health} health.");
        else
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
        Console.WriteLine($"Your score is: {Score.CalculateScore(false)}");
        Game.Singleton.GameHasEnded = true;
    }
    #endregion
}