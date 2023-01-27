//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public class DungeonNavigation
{
    public event Action OnPlayerEnteredNewRoom = delegate {  };
    
    private Dungeon _dungeon;
    private Player _player;
    
    public DungeonNavigation(Dungeon dungeon, Player player)
    {
        _dungeon = dungeon;
        _player = player;
        
        _dungeon.StartingRoom.Enter();
    }

    
    public bool Go(Direction direction)
    {
        //if the door exists ...
        if (_player.CurrentRoom.HasDoorAt(direction))
        {
            //... enter the room
            Console.WriteLine($"You decide to go {direction.ToString().ToLower()}.");
            Room newRoom = _player.CurrentRoom.GetAdjacentRoomAt(direction);
            _player.CurrentRoom = newRoom;
            newRoom.Enter();
            OnPlayerEnteredNewRoom?.Invoke();
            return true;
        }
        
        //if the door doesn't exist...

        //... let the player know
        Console.WriteLine($"You walk {direction.ToString().ToLower()}, but there is no door. You hit your head against the wall and feel a bit stupid. How will someone who runs into walls defeat a lindworm?");
        _player.DealDamage(1);
        return false;
    }

    public void Go(List<string> userParameters)
    {
        var direction = Direction.NONE;
        
        //try to parse a valid direction from the given parameters
        foreach (string word in userParameters)
        {
            direction = StringParser.DirectionFromString(word);
            if (direction != Direction.NONE) break;
        }
        
        //if no valid direction was found, we can't travel
        if (direction == Direction.NONE)
        {
            Console.WriteLine("In which direction do you want to go? Please type e.g. \"go north\".");
            return;
        }

        //if a valid direction was found, travel
        Go(direction);
    }
}