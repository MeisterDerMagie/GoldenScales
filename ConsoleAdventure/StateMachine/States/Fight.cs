﻿//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.Enemies;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class Fight : IState
{
    public List<Command> AvailableCommands { get; set; }
    
    private readonly Player _player;
    private readonly Enemy _enemy;
    private List<Command> _availableCommands = new List<Command>();

    public Fight(Player player, Enemy enemy)
    {
        AvailableCommands = new List<Command>();
        _player = player;
        _enemy = enemy;
    }

    public void OnEnter()
    {
        //add commands
        AddFightCommands();
        
        //randomly determine, if the player or the enemy attacks first
        
    }

    public void Tick()
    {
        string userInput = ConsoleUtilities.InputString("What will be your next move?");
        if (CommandUtilities.TryExecuteUserInput(userInput, _availableCommands) == false)
        {
            Console.WriteLine("Type \"options\" to see a list of all actions you can do right now.");
        }
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }

    private void AddFightCommands()
    {
        throw new NotImplementedException();
    }
}