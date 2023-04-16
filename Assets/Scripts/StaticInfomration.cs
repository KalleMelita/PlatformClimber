public static class StaticInfomration
{
    public static readonly float velocity = 20;
    public static float cameraLeftCoordinates = -8;
    public static float cameraRightCoordinates = 8;
    public static float width(){
        return System.Math.Abs(cameraLeftCoordinates) + System.Math.Abs(cameraRightCoordinates);
    }
    public static float cameraHeight = 20;

    public static string SCENE_PLAY = "PlayScene";

    public static string SCENE_MENU = "Menu";

    /// <summary>
    /// Player Pref Tag under which the Player Name is stored.
    /// </summary>
    public static string PLAYER_PREF_TAG = "playerPref";
}