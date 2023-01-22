//(c) copyright by Martin M. Klöckener

using ConsoleAdventure.Rooms;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public static class Constants
{
    #region Enemies
    public const int SkeletonMaxHealth = 80;
    public const int WizardMaxHealth = 45;
    public const int SpiderMaxHealth = 60;
    public const int MimicMaxHealth = 70;
    #endregion
    
    #region Map
    public const string MapPlayerPosition = "X";
    public const string MapRoom = "O";
    public const string MapRoomTrader = "$";
    public const string MapRoomHealingWell = "W";
    public const string MapRoomBoss = "B";
    public const string MapRoomNone = " ";
    public const string MapDoorHorizontal = "--";
    public const string MapDoorHorizontalNone = "  ";
    public const string MapDoorVertical = "|";
    public const string MapDoorVerticalNone = " ";
    #endregion
    
    #region DungeonGeneration
    public const float ProbabilityForDoorToAlreadyExistingAdjacentRoom = 0.3f;
    
    private const float ProbabilityRoomNorth = 0.25f;
    private const float ProbabilityRoomEast = 0.25f;
    private const float ProbabilityRoomSouth = 0.25f;
    private const float ProbabilityRoomWest = 0.25f;
    public static readonly List<ElementProbability<Direction>> RoomDirectionProbabilitiesBranches = new() { new ElementProbability<Direction>(Direction.North, ProbabilityRoomNorth), new ElementProbability<Direction>(Direction.East, ProbabilityRoomEast), new ElementProbability<Direction>(Direction.South, ProbabilityRoomSouth), new ElementProbability<Direction>(Direction.West, ProbabilityRoomWest) };

    private const float ProbabilityMainPathRoomNorth = 0.1f;
    private const float ProbabilityMainPathRoomEast = 0.6f;
    private const float ProbabilityMainPathRoomSouth = 0.3f;
    private const float ProbabilityMainPathRoomWest = 0f;
    public static readonly List<ElementProbability<Direction>> RoomDirectionProbabilitiesMainBranch = new() { new ElementProbability<Direction>(Direction.North, ProbabilityMainPathRoomNorth), new ElementProbability<Direction>(Direction.East, ProbabilityMainPathRoomEast), new ElementProbability<Direction>(Direction.South, ProbabilityMainPathRoomSouth), new ElementProbability<Direction>(Direction.West, ProbabilityMainPathRoomWest) };

    private const float ProbabilityEmptyRoom = 0.15f;
    private const float ProbabilityEnemyRoom = 0.33f;
    private const float ProbabilityTraderRoom = 0.07f;
    private const float ProbabilityTreasureRoom = 0.1f;
    private const float ProbabilityChestRoom = 0.18f;
    private const float ProbabilityMimicRoom = 0.02f;
    private const float ProbabilityTrapRoom = 0.075f;
    private const float ProbabilityHealingWellRoom = 0.075f;
    public static readonly List<ElementProbability<RoomType>> RoomTypeProbabilities = new() { new ElementProbability<RoomType>(RoomType.ChestRoom, ProbabilityChestRoom), new ElementProbability<RoomType>(RoomType.EmptyRoom, ProbabilityEmptyRoom), new ElementProbability<RoomType>(RoomType.EnemyRoom, ProbabilityEnemyRoom), new ElementProbability<RoomType>(RoomType.TraderRoom, ProbabilityTraderRoom), new ElementProbability<RoomType>(RoomType.TreasureRoom, ProbabilityTreasureRoom), new ElementProbability<RoomType>(RoomType.TrapRoom, ProbabilityTrapRoom), new ElementProbability<RoomType>(RoomType.HealingWellRoom, ProbabilityHealingWellRoom), new ElementProbability<RoomType>(RoomType.MimicRoom, ProbabilityMimicRoom)};

    #endregion
}