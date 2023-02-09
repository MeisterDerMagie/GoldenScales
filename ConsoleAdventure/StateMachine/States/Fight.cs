//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Enemies;
using ConsoleAdventure.Items;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Fight : IState
{
    public List<Command> AvailableCommands { get; set; }
    public string TextWhenReturningToThisState => $"You're back to the fight against the {_enemy.Name}.";
    
    private readonly Enemy _enemy;
    
    private TimeSpan _enemyTimeSinceLastAttack = TimeSpan.Zero;
    private TimeSpan _playerTimeSinceLastAttack = TimeSpan.Zero;

    public Fight(Enemy enemy)
    {
        AvailableCommands = new List<Command>();
        _enemy = enemy;
    }


    public void OnEnter()
    {
        //add commands
        AddFightCommands();
    }

    public void Tick()
    {
        string userInput = ConsoleUtilities.InputString("What will be your next move?");
        if (CommandUtilities.TryExecuteUserInput(userInput, AvailableCommands) == false)
        {
            Console.WriteLine("Type \"options\" to see a list of all actions you can do right now.");
        }
    }

    public void OnExit()
    {
        
    }

    private void FightLoop()
    {
        Console.WriteLine("The fight goes on...");
        
        DateTime lastLoop = DateTime.Now;
        DateTime loopStart = DateTime.Now;
        int loopGoingForSeconds = 0;
        while (_playerTimeSinceLastAttack.TotalSeconds < Player.Singleton.Weapon.AttackDuration)
        {
            //print dots (......) to show the player that there's something happening
            if (loopGoingForSeconds != (int)(loopStart - DateTime.Now).TotalSeconds)
            {
                Console.Write('.');
                loopGoingForSeconds = (int)(loopStart - DateTime.Now).TotalSeconds;
            }
            
            //update timers
            _playerTimeSinceLastAttack += DateTime.Now - lastLoop;
            _enemyTimeSinceLastAttack += DateTime.Now - lastLoop;

            //enemy attack
            if (_enemyTimeSinceLastAttack.TotalSeconds >= _enemy.Weapon.AttackDuration)
            {
                int enemyAttackDamage = _enemy.Weapon.GetDamage();
                Console.WriteLine($"The {_enemy.Name} attacks you!");
                Player.Singleton.DealDamage(enemyAttackDamage);
                if (Player.Singleton.Health == 0) return;
                _enemyTimeSinceLastAttack = TimeSpan.Zero;
            }
            
            lastLoop = DateTime.Now;
        }

        //if player timer is up, attack enemy
        int attackDamage = Player.Singleton.Weapon.GetDamage();
        Console.WriteLine($"You attack the {_enemy.Name} with your {Player.Singleton.Weapon.Name} for {attackDamage} damage. It now has {((_enemy.Health - attackDamage) < 0 ? 0 : (_enemy.Health - attackDamage))} health left.");
        _enemy.DealDamage(attackDamage);
        _playerTimeSinceLastAttack = TimeSpan.Zero;

        //check if enemy died (not for the lindworm)
        if (_enemy is not Lindworm && _enemy.Health <= 0)
        {
            Console.WriteLine($"You defeat the {_enemy.Name} after an intense battle. Now you can continue to explore the dungeon.");
            
            //return to exploration
            Game.StateMachine.SetState(Game.ExplorationState, false);
        }
    }

    private void AddFightCommands()
    {
        var attackCommand = new Command("attack (attack your enemy)", new List<string> { "attack" }, FightLoop);
        var openInventoryCommand = new Command("open inventory (open your inventory)", new List<string> { "open inventory", "inventory" }, Player.Singleton.OpenInventory);
        
        AvailableCommands.Add(attackCommand);
        AvailableCommands.Add(openInventoryCommand);
    }
}