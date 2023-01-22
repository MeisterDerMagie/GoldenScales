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
                break;
            case RoomType.StartRoom:
                return new StartRoom(roomPosition);
                break;
            case RoomType.EmptyRoom:
                return new EmptyRoom(roomPosition);
                break;
            case RoomType.EnemyRoom:
                return new EnemyRoom(roomPosition);
                break;
            case RoomType.TraderRoom:
                return new TraderRoom(roomPosition);
                break;
            case RoomType.TreasureRoom:
                return new TreasureRoom(roomPosition);
                break;
            case RoomType.ChestRoom:
                return new ChestRoom(roomPosition);
                break;
            case RoomType.MimicRoom:
                return new MimicRoom(roomPosition);
                break;
            case RoomType.BossRoom:
                return new BossRoom(roomPosition);
                break;
            case RoomType.TrapRoom:
                return new TrapRoom(roomPosition);
                break;
            case RoomType.HealingWellRoom:
                return new HealingWellRoom(roomPosition);
                break;
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