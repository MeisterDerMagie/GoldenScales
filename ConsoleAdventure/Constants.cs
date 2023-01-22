//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public static class Constants
{
    #region Enemies
    public const int SkeletonMaxHealth = 80;
    public const int WizardMaxHealth = 45;
    public const int SpiderMaxHealth = 60;
    #endregion
    
    #region Map
    public const string MapPlayerPosition = "X";
    public const string MapRoom = "O";
    public const string MapRoomTrader = "$";
    public const string MapRoomBoss = "B";
    public const string MapRoomNone = " ";
    public const string MapDoorHorizontal = "--";
    public const string MapDoorHorizontalNone = "  ";
    public const string MapDoorVertical = "|";
    public const string MapDoorVerticalNone = " ";
    #endregion
    
    #region DungeonGeneration
    public const float ProbabilityForDoorToAlreadyExistingAdjacentRoom = 0.3f;
    public const float ProbabilityRoomNorth = 0.25f;
    public const float ProbabilityRoomEast = 0.25f;
    public const float ProbabilityRoomSouth = 0.25f;
    public const float ProbabilityRoomWest = 0.25f;
    
    public const float ProbabilityMainPathRoomNorth = 0.1f;
    public const float ProbabilityMainPathRoomEast = 0.6f;
    public const float ProbabilityMainPathRoomSouth = 0.3f;
    public const float ProbabilityMainPathRoomWest = 0f;
    #endregion
}