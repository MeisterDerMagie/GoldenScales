//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public class DungeonNavigation
{
    private Dungeon _dungeon;
    private Player _player;
    
    public DungeonNavigation(Dungeon dungeon, Player player)
    {
        _dungeon = dungeon;
        _player = player;
        
        _dungeon.StartingRoom.Enter();
    }

    public bool Travel(Direction direction)
    {
        //if the door exists ...
        if (_player.CurrentRoom.HasDoorAt(direction))
        {
            //... enter the room
            Console.WriteLine($"You decide to go {direction.ToString().ToLower()}.");
            _player.CurrentRoom.GetAdjacentRoomAt(direction).Enter();
            return true;
        }
        
        //if the door doesn't exist...
        else
        {
            //... let the player know
            Console.WriteLine($"You walk {direction.ToString().ToLower()}, but there is no door. You hit your head against the wall and feel a bit stupid. How will someone who runs into walls defeat a lindworm?");
            _player.DealDamage(1);
            return false;
        }
    }
}