using UnityEngine;

public static class PlayerDataManager
{
    public static void CheckKey()
    {
        if(!PlayerPrefs.HasKey("LastLevel"))
        {
            PlayerPrefs.SetInt("LastLevel", 1);
            PlayerPrefs.SetInt("Point", 0);
        }
    }
    public static void SetLastLevel(int levelValue)
    {
        PlayerPrefs.SetInt("LastLevel",levelValue);
        PlayerPrefs.Save();
    }
    public static int GetLastLevel()
    {
        return PlayerPrefs.GetInt("LastLevel");
    }

}
