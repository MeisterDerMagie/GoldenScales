//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public static class DungeonGenerator
{
    private static Dungeon dungeon;
    private static int generatedRooms = 0;
    
    public static Dungeon Generate(int mainPathLength, int subBranches, int seed)
    {
        //set seed
        RandomUtilities.SetSeed(seed);

        dungeon = new Dungeon();

        generatedRooms = 1;

        //first room
        var roomPosition = new RoomPosition(0, 0);
        var firstRoom = new Room("Room", roomPosition);
        dungeon.AddRoom(firstRoom);

        //generate main path
        List<Room> mainPath = GenerateBranch(firstRoom, mainPathLength, Constants.ProbabilityMainPathRoomNorth, Constants.ProbabilityMainPathRoomEast, Constants.ProbabilityMainPathRoomSouth, Constants.ProbabilityMainPathRoomWest);
        mainPath[^1].IsBossRoom = true;

        //generate sub branches
        for (int i = 0; i < subBranches; i++)
        {
            Room randomRoom = null;

            do
            {
                randomRoom = RandomUtilities.RandomRoom(dungeon);
            }
            while (randomRoom.IsBossRoom);
            
            int minBranchLength = 1;
            int maxBranchLength = Convert.ToInt32(mainPathLength / 4f);
            int branchLength = RandomUtilities.RandomInt(minBranchLength, maxBranchLength + 1);
            GenerateBranch(randomRoom, branchLength, Constants.ProbabilityRoomNorth, Constants.ProbabilityRoomEast, Constants.ProbabilityRoomSouth, Constants.ProbabilityRoomWest);
        }

        Console.WriteLine($"Generated {generatedRooms.ToString()} rooms.");
        
        return dungeon;
    }

    private static List<Room> GenerateBranch(Room root, int branchLength, float probabilityNorth, float probabilityEast, float probabilitySouth, float probabilityWest)
    {
        List<Room> branch = new(){root};

        for (int i = 0; i < branchLength; i++)
        {
            Room newRoom = GenerateAdjacentRoom(branch[i], null, probabilityNorth, probabilityEast, probabilitySouth, probabilityWest);
            if (newRoom == null) return branch;
            
            branch.Add(newRoom);
        }

        return branch;
    }

    private static Room GenerateAdjacentRoom(Room currentRoom, Room parentRoom, float probabilityNorth, float probabilityEast, float probabilitySouth, float probabilityWest)
    {
        if (currentRoom == null) return null;
        
        var adjacentEmptyTiles = new List<(RoomPosition roomPosition, Direction direction)>();
        var adjacentRooms = new List<Room>(); //excluding the parent room
        
        //-- Check North --
        if (currentRoom.Position.Y != 0)
        {
            var northPosition = new RoomPosition(currentRoom.Position.X, currentRoom.Position.Y - 1);
            CheckAdjacentPosition(northPosition, Direction.North);
        }
        
        //-- Check East --
        var eastPosition = new RoomPosition(currentRoom.Position.X + 1, currentRoom.Position.Y);
        CheckAdjacentPosition(eastPosition, Direction.East);

        //-- Check South --
        var southPosition = new RoomPosition(currentRoom.Position.X, currentRoom.Position.Y + 1);
        CheckAdjacentPosition(southPosition, Direction.South);

        //-- Check West --
        if (currentRoom.Position.X != 0)
        {
            var westPosition = new RoomPosition(currentRoom.Position.X - 1, currentRoom.Position.Y);
            CheckAdjacentPosition(westPosition, Direction.West);
        }
        
        //if we have 3 adjacent rooms this means that we already have a room on all sides -> return
        if (adjacentRooms.Count == 3)
        {
            return null;
        }
        
        //-- Generate doors for already existing adjacent rooms (with a given probability) --
        foreach (Room adjacentRoom in adjacentRooms)
        {
            if(dungeon.DoorExists(currentRoom, adjacentRoom)) continue;

            if (RandomUtilities.RandomBoolWeighted(Constants.ProbabilityForDoorToAlreadyExistingAdjacentRoom))
            {
                var door = new Door(currentRoom, adjacentRoom, false);
                dungeon.AddDoor(door);
            }
        }

        //-- Generate adjacent room(s) --
        return GenerateRoom();
        
        
        //room generation code. the more branched we are, the less likely it is to generate an adjacent room
        Room GenerateRoom()
        {
            Direction randomDirection = Direction.North;
            bool directionIsEmpty = false;

            while (!directionIsEmpty)
            {
                if (adjacentEmptyTiles.Count == 0) return null;
                
                randomDirection = RandomUtilities.RandomDirection(probabilityNorth, probabilityEast, probabilitySouth, probabilityWest);
                foreach (var adjacentEmptyTile in adjacentEmptyTiles)
                {
                    if (adjacentEmptyTile.direction == randomDirection) directionIsEmpty = true;
                }
            }

            RoomPosition roomPosition;
            
            switch (randomDirection)
            {
                case Direction.North:
                    roomPosition = currentRoom.Position.North;
                    break;
                case Direction.East:
                    roomPosition = currentRoom.Position.East;
                    break;
                case Direction.South:
                    roomPosition = currentRoom.Position.South;
                    break;
                case Direction.West:
                    roomPosition = currentRoom.Position.West;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            var newRoom = new Room("Room", roomPosition);
            var newDoor = new Door(currentRoom, newRoom, false);
            
            dungeon.AddRoom(newRoom);
            dungeon.AddDoor(newDoor);

            generatedRooms += 1;
            
            return newRoom;
        }
        
        void CheckAdjacentPosition(RoomPosition positionToCheck, Direction direction)
        {
            //if the adjacent position is the parent room, ignore it
            if (parentRoom != null && positionToCheck == parentRoom.Position)
                return;

            //if there already is a room add it to the adjacentRooms list
            if (dungeon.RoomExists(positionToCheck))
                adjacentRooms.Add(dungeon.GetRoomByPosition(positionToCheck));

            //... otherwise, if there is no room, add it to the emptyTiles list
            else
                adjacentEmptyTiles.Add((positionToCheck, direction));
        }
    }
}