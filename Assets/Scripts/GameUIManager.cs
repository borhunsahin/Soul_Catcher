using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    GameManager gameManager;

    public GamePanels gamePanels;

    AudioSource audioSource;
    public GameSounds gameSounds;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();

        gameSounds.VolumeSlider.value = PlayerDataManager.GetVolume();
        gameSounds.SoundToggle.isOn = PlayerDataManager.GetSound();
        gameSounds.MusicToggle.isOn = PlayerDataManager.GetMusic();
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
        gamePanels.pausePanel.SetActive(true);
        audioSource.PlayOneShot(gameSounds.ButtonSound);
    }
    public void ContinueButton()
    {
        Time.timeScale = 1;
        gamePanels.pausePanel.SetActive(false);
        audioSource.PlayOneShot(gameSounds.ButtonSound);
    }
    public void SettingsButton()
    {
        gamePanels.settingsPanel.SetActive(true);
        audioSource.PlayOneShot(gameSounds.ButtonSound);
    }
    public void BackButton()
    {
        gamePanels.settingsPanel.SetActive(false);
        audioSource.PlayOneShot(gameSounds.ButtonSound);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        audioSource.PlayOneShot(gameSounds.ButtonSound);
    }
    public void ExitButton()
    {
        gamePanels.exitQuaryPanel.SetActive(true);
        audioSource.PlayOneShot(gameSounds.ExitButtonSound);
    }
    public void StayButton()
    {
        gamePanels.exitQuaryPanel.SetActive(false);
        audioSource.PlayOneShot(gameSounds.ButtonSound);
    }
    public void LeaveButton()
    {
        audioSource.PlayOneShot(gameSounds.ExitButtonSound);
        Application.Quit();
    }
    public void VolumeSlider()
    {
        audioSource.volume = gameSounds.VolumeSlider.value;
        gameSounds.gameMusic.volume = gameSounds.VolumeSlider.value;
        PlayerDataManager.SetVolume(gameSounds.VolumeSlider.value);
    }
    public void SoundToggle()
    {
        audioSource.mute = gameSounds.SoundToggle.isOn;
        gameSounds.gameMusic.mute = gameSounds.SoundToggle.isOn;
        gameSounds.MusicToggle.isOn = gameSounds.SoundToggle.isOn;
        PlayerDataManager.SetSound(gameSounds.SoundToggle.isOn);
        PlayerDataManager.SetMusic(gameSounds.MusicToggle.isOn);
    }
    public void MusicToggle()
    {
        if (!audioSource.mute)
        {
            gameSounds.gameMusic.mute = gameSounds.MusicToggle.isOn;
            PlayerDataManager.SetMusic(gameSounds.MusicToggle.isOn);
        }
    }
}
[Serializable]
public struct GamePanels
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject exitQuaryPanel;
}
[Serializable]
public struct GameSounds
{
    public Slider VolumeSlider;
    public Toggle SoundToggle;
    public Toggle MusicToggle;

    public AudioClip ButtonSound;
    public AudioClip ExitButtonSound;

    public AudioSource gameMusic;
}
