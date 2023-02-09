//(c) copyright by Martin M. Klöckener
using ConsoleAdventure.DataTypes;
using ConsoleAdventure.Rooms;
using ConsoleAdventure.Utilities;

namespace ConsoleAdventure;

public static class Constants
{
    #region Enemies
    public const int SkeletonMaxHealth = 50;
    public const int WizardMaxHealth = 20;
    public const int SpiderMaxHealth = 30;
    public const int MimicMaxHealth = 35;
    public const int LindwormMaxHealth = 100;
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
    
    #region Damage
    public static Range<int> TrapDamage = new(3, 10);
    public const int ArmorCoefficient = 25; //change this, to affect how much damage armor will negate. The lower the value, the more it will negate.
    #endregion

    #region Gold Values
    public const int ValueRareDiamondInEmptyRoom = 40;
    public static Range<int> ChestValueRange => new(3, 20);
    public static Range<int> TreasureRoomGoldRange => new(5, 22);
    public static Range<int> TreasureRoomItemValueRange => new(3, 20);
    public static Range<int> TraderItemAmount => new Range<int>(2, 6);
    public static Range<int> TraderItemValue => new Range<int>(3, 16);
    public static Range<int> EnemyLootGoldValueRange => new Range<int>(5, 22);
    #endregion

    #region Equipment Gold Values
    public const float ArmorGoldPerProtection = 2f;
    public const float WeaponGoldPerDps = 3f;
    #endregion
    
    #region Dungeon Generation
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
    
    /*
    private const float ProbabilityEmptyRoom = 0f;
    private const float ProbabilityEnemyRoom = 1f;
    private const float ProbabilityTraderRoom = 0f;
    private const float ProbabilityTreasureRoom = 0f;
    private const float ProbabilityChestRoom = 0f;
    private const float ProbabilityMimicRoom = 0f;
    private const float ProbabilityTrapRoom = 0f;
    private const float ProbabilityHealingWellRoom = 0f;
    */
    
    public static readonly List<ElementProbability<RoomType>> RoomTypeProbabilities = new() { new ElementProbability<RoomType>(RoomType.ChestRoom, ProbabilityChestRoom), new ElementProbability<RoomType>(RoomType.EmptyRoom, ProbabilityEmptyRoom), new ElementProbability<RoomType>(RoomType.EnemyRoom, ProbabilityEnemyRoom), new ElementProbability<RoomType>(RoomType.TraderRoom, ProbabilityTraderRoom), new ElementProbability<RoomType>(RoomType.TreasureRoom, ProbabilityTreasureRoom), new ElementProbability<RoomType>(RoomType.TrapRoom, ProbabilityTrapRoom), new ElementProbability<RoomType>(RoomType.HealingWellRoom, ProbabilityHealingWellRoom), new ElementProbability<RoomType>(RoomType.MimicRoom, ProbabilityMimicRoom)};

    #endregion
}