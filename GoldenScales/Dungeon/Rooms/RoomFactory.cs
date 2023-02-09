//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(RoomType roomType, RoomPosition roomPosition)
    {
        switch (roomType)
        {
            case RoomType.NONE:
                throw new Exception("Please specify a room type.");
            case RoomType.StartRoom:
                return new StartRoom(roomPosition);
            case RoomType.EmptyRoom:
                return new EmptyRoom(roomPosition);
            case RoomType.EnemyRoom:
                return new EnemyRoom(roomPosition);
            case RoomType.TraderRoom:
                return new TraderRoom(roomPosition);
            case RoomType.TreasureRoom:
                return new TreasureRoom(roomPosition);
            case RoomType.ChestRoom:
                return new ChestRoom(roomPosition);
            case RoomType.MimicRoom:
                return new MimicRoom(roomPosition);
            case RoomType.BossRoom:
                return new BossRoom(roomPosition);
            case RoomType.TrapRoom:
                return new TrapRoom(roomPosition);
            case RoomType.HealingWellRoom:
                return new HealingWellRoom(roomPosition);
            default:
                throw new ArgumentOutOfRangeException(nameof(roomType), roomType, null);
        }
    }
}
public enum RoomType
{
    NONE,
    StartRoom,
    BossRoom,
    EmptyRoom,
    EnemyRoom,
    TraderRoom,
    TreasureRoom,
    ChestRoom,
    MimicRoom,
    TrapRoom,
    HealingWellRoom
}