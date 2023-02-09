//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public class EmptyRoom : Room
{
    private bool _enteredForTheFirstTime = true;
    private bool _foundDiamondInThisRoom;
    
    protected override string EnterText =>
        _foundDiamondInThisRoom
            ? "In this room you found a diamond. Maybe there are more lying around? You start searching, but it seems that the one you found was the only one lying here."
            : "You enter a room that has nothing to offer except dust and cobwebs. How boring.";
    
    public EmptyRoom(RoomPosition position) : base("Empty Room", position)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        //there's nothing to do here...
        if (!_enteredForTheFirstTime) return;
        _enteredForTheFirstTime = false;
        
        //... except for a 2% chance to find a valuable diamond the first time you enter the room
        var rng = new Random();
        float random = rng.NextSingle();
        if (random > 0.02f) return;
        
        Console.WriteLine("At second glance, you notice a slight sparkle in the corner of the room. What is that? Half covered by dust, you find a valuable diamond! Who might have lost it here? What luck for you!");
        Player.Singleton.AddGold(Constants.ValueRareDiamondInEmptyRoom);

        _foundDiamondInThisRoom = true;
    }
}