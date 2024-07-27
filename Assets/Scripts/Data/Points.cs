using UnityEngine;

public static class Points
{
    public static bool SetPoints(int points)
    {
        int currentValue = GetPoints();

        if(points > currentValue)
        {
            PlayerPrefs.SetInt(Constants.Points.PREFSKEY, points);
            return true;
        }

        return false;
    }

    public static int GetPoints()
    {
        return PlayerPrefs.GetInt(Constants.Points.PREFSKEY);
    }
}
