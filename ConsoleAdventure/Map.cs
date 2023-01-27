//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public static class Map
{
    public static void Draw(Dungeon dungeon, Player player, bool drawDiscoveredElementsOnly)
    {
        Console.WriteLine("");
        
        int highestXCoordinate = 0;
        int highestYCoordinate = 0;

        foreach (var room in dungeon.Rooms)
        {
            if(drawDiscoveredElementsOnly && !room.Discovered) continue;
            
            if (room.Position.X > highestXCoordinate) highestXCoordinate = (int)room.Position.X;
            if (room.Position.Y > highestYCoordinate) highestYCoordinate = (int)room.Position.Y;
        }

        //y axis
        for (uint y = 0; y <= highestYCoordinate; y++)
        {
            string southDoors = string.Empty;

            //x axis
            for (uint x = 0; x <= highestXCoordinate; x++)
            {
                if (!dungeon.RoomExists(x, y))
                {
                    Console.Write(Constants.MapRoomNone);
                    Console.Write(Constants.MapDoorHorizontalNone);
                    
                    southDoors += Constants.MapDoorVerticalNone + Constants.MapDoorHorizontalNone;
                }

                else
                {
                    Room room = dungeon.GetRoomByPosition(x, y);
                    bool playerIsInThisRoom = room.Position == player.CurrentRoom.Position;
                    //display player icon
                    string roomIcon = playerIsInThisRoom ? Constants.MapPlayerPosition : room.RoomMapIcon;
                    //don't show room if it hasn't been discovered
                    if(drawDiscoveredElementsOnly) roomIcon = room.Discovered ? roomIcon : Constants.MapRoomNone;
                    Console.Write(roomIcon);
                    
                    //draw door to the right (east)
                    bool eastDoorExists = false;
                    bool southDoorExists = false;
                    var eastPosition = new RoomPosition(room.Position.X + 1, room.Position.Y);
                    var southPosition = new RoomPosition(room.Position.X, room.Position.Y + 1);
                    bool eastDoorDiscovered = false;
                    bool southDoorDiscovered = false;
                    foreach (Door door in room.LinkedDoors)
                    {
                        Room otherRoom = door.Source == room ? door.Target : door.Source;
                        if (otherRoom.Position == eastPosition)
                        {
                            eastDoorExists = true;
                            eastDoorDiscovered = door.Discovered;
                        }

                        if (otherRoom.Position == southPosition)
                        {
                            southDoorExists = true;
                            southDoorDiscovered = door.Discovered;
                        }
                    }

                    string consoleOutput = eastDoorExists ? Constants.MapDoorHorizontal : Constants.MapDoorHorizontalNone;
                    if (drawDiscoveredElementsOnly) consoleOutput = eastDoorDiscovered ? consoleOutput : Constants.MapDoorHorizontalNone;
                    Console.Write(consoleOutput);

                    //South doors
                    string southDoor = southDoorExists
                        ? Constants.MapDoorVertical + Constants.MapDoorHorizontalNone
                        : Constants.MapDoorVerticalNone + Constants.MapDoorHorizontalNone;
                    if(drawDiscoveredElementsOnly) southDoor = southDoorDiscovered ? southDoor : Constants.MapDoorVerticalNone + Constants.MapDoorHorizontalNone;
                    southDoors += southDoor;
                }
            }

            Console.Write("\n" + southDoors + "\n");
        }
        
        Console.WriteLine("\n");
    }
}