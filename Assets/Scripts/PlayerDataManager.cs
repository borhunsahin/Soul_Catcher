using System.Drawing;
using UnityEngine;

public static class PlayerDataManager
{
    public static void SetActiveScene(string key,int value)
    {

    }
    //public static void GetActiveScene(string key, int value)
    //{

    //}
    public static void SetPoint(string key, int value)
    {
        PlayerPrefs.GetInt(key, value);
    }
    public static int GetPoint(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
}
