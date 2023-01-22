//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Rooms;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public static class DungeonGenerator
{
    private static Dungeon dungeon;
    private static int generatedRooms = 0;
    private static List<RoomType> generatedRoomTypes = new();

    public static Dungeon Generate(int mainPathLength, int subBranches, int seed)
    {
        //set seed
        RandomUtilities.SetSeed(seed);

        dungeon = new Dungeon();

        generatedRooms = 1;
        generatedRoomTypes.Clear();

        //first room
        var roomPosition = new RoomPosition(0, 0);
        var firstRoom = RoomFactory.CreateRoom(RoomType.StartRoom, roomPosition);
        dungeon.AddRoom(firstRoom);
        dungeon.StartingRoom = firstRoom;
        generatedRoomTypes.Add(RoomType.StartRoom);

        //generate main path
        List<Room> mainPath = GenerateBranch(firstRoom, mainPathLength, true, Constants.RoomDirectionProbabilitiesMainBranch);

        //generate sub branches
        for (int i = 0; i < subBranches; i++)
        {
            Room randomRoom = null;

            do
            {
                randomRoom = RandomUtilities.RandomRoom(dungeon);
            }
            while (randomRoom is BossRoom);
            
            int minBranchLength = 1;
            int maxBranchLength = Convert.ToInt32(mainPathLength / 4f);
            int branchLength = RandomUtilities.RandomInt(minBranchLength, maxBranchLength + 1);
            GenerateBranch(randomRoom, branchLength, false, Constants.RoomDirectionProbabilitiesBranches);
        }

        Console.WriteLine($"Generated {generatedRooms.ToString()} total rooms.");
        foreach (RoomType roomType in (RoomType[]) Enum.GetValues(typeof(RoomType)))
        {
            OutputAmountOfGeneratedRoomTypes(roomType);
        }

        return dungeon;
    }

    private static void OutputAmountOfGeneratedRoomTypes(RoomType roomType)
    {
        int roomTypeCount = generatedRoomTypes.Count(rt => (rt == roomType));
        Console.WriteLine($"Dungeon has {roomTypeCount} {roomType}s.");
    }

    private static List<Room> GenerateBranch(Room root, int branchLength, bool isMainBranch, List<ElementProbability<Direction>> directionProbabilities)
    {
        List<Room> branch = new(){root};

        for (int i = 0; i < branchLength; i++)
        {
            //if we generate the main branch and it's the last room, create a boss room. otherwise create a random room
            RoomType roomType = (isMainBranch && i == branchLength - 1)
                ? RoomType.BossRoom
                : RoomType.NONE;
            
            Room newRoom = GenerateAdjacentRoom(branch[i], null, directionProbabilities, roomType);
            if (newRoom == null) return branch;
            if (roomType == RoomType.BossRoom)
                dungeon.BossRoom = newRoom;
            
            branch.Add(newRoom);
        }

        return branch;
    }

    private static Room GenerateAdjacentRoom(Room currentRoom, Room parentRoom, List<ElementProbability<Direction>> directionProbabilities, RoomType roomType = RoomType.NONE)
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
                
                randomDirection = RandomUtilities.RandomElement(directionProbabilities);
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
            
            //if no room type was specified: generate a random room
            Room newRoom;
            if (roomType == RoomType.NONE)
            {
                //Generate room of random type
                RoomType randomRoomType = RandomUtilities.RandomElement(Constants.RoomTypeProbabilities); 
                newRoom = RoomFactory.CreateRoom(randomRoomType, roomPosition);
                generatedRoomTypes.Add(randomRoomType);
            }
            //otherwise generate a room of the specified type
            else
            {
                newRoom = RoomFactory.CreateRoom(roomType, roomPosition);
                generatedRoomTypes.Add(roomType);
            }
            
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