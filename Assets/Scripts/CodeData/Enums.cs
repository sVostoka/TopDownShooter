using System;

public static class Enums
{
    #region -Serializable-
    [Serializable]
    public enum Scenes
    {
        MainMenu = 0,
        GameScene = 1,
    }
    #endregion

    #region -NonSerializable-

    public enum Axis
    {
        Horizontal,
        Vertical,
    }

    public enum Side
    {
        Top,
        Bottom,
        Left,
        Right,
    }

    public enum Tags
    {
        Level
    }
    #endregion
}