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
    public static void SetVolume(float volumeValue)
    {
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        PlayerPrefs.Save();
    }
    public static float GetVolume()
    {
        return PlayerPrefs.GetFloat("VolumeValue");
    }
    public static void SetSound(bool soundToggle)
    {
        PlayerPrefs.SetInt("SoundToggle", (soundToggle ? 0:1));
        PlayerPrefs.Save();
    }
    public static bool GetSound()
    {
        return PlayerPrefs.GetInt("SoundToggle", 0) == 0;
    }
    public static void SetMusic(bool musicToggle)
    {
        PlayerPrefs.SetInt("MusicToggle", (musicToggle ? 0:1));
        PlayerPrefs.Save();
    }
    public static bool GetMusic()
    {
        return PlayerPrefs.GetInt("MusicToggle", 0) == 0;
    }
}
