using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public MainMenuPanels mainMenuPanels;
    private List<GameObject> panelList;

    private AudioSource audioSource;
    public MainMenuSounds mainMenuSounds;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        panelList = new List<GameObject>
        {
            mainMenuPanels.MainMenuPanel,
            mainMenuPanels.SettingsPanel,
            mainMenuPanels.NoAdsPanel,
            mainMenuPanels.ExitQuaryPanel,
            mainMenuPanels.PlayPanel,
            mainMenuPanels.LevelSelectPanel,
            mainMenuPanels.LevelSelectBackPanel
        };

        PlayerDataManager.CheckKey();

        mainMenuSounds.VolumeSlider.value = PlayerDataManager.GetVolume();
        mainMenuSounds.SoundToggle.isOn = PlayerDataManager.GetSound();
        mainMenuSounds.MusicToggle.isOn = PlayerDataManager.GetMusic();
    }

    private void PanelSelecter(GameObject panelName) 
    {
        
        foreach (GameObject panel in panelList)
        {
            if(panel.name == panelName.name)
            {
                panel.SetActive(true);
            }
            else
            {
                if(panel.name != mainMenuPanels.MainMenuPanel.name)
                    panel.SetActive(false);
            }
        }
    }
    public void ContinueButton()
    {
        SceneManager.LoadScene(PlayerDataManager.GetLastLevel());
        
    }
    public void PlayButton()
    {
        PanelSelecter(mainMenuPanels.PlayPanel);
        audioSource.PlayOneShot(mainMenuSounds.ButtonSound);
    }
    public void SandBoxButton()
    {

        audioSource.PlayOneShot(mainMenuSounds.ButtonSound);
    }
    public void LevelSelecterButton()
    {
        PanelSelecter(mainMenuPanels.LevelSelectPanel);
        audioSource.PlayOneShot(mainMenuSounds.ButtonSound);
    }
    public void SettingsButton()
    {
        PanelSelecter(mainMenuPanels.SettingsPanel);
        audioSource.PlayOneShot(mainMenuSounds.ButtonSound);
    }
    public void NoADSButton()
    {
        PanelSelecter(mainMenuPanels.NoAdsPanel);
        audioSource.PlayOneShot(mainMenuSounds.ButtonSound);
    }
    public void ExitButton()
    {
        PanelSelecter(mainMenuPanels.ExitQuaryPanel);
        audioSource.PlayOneShot(mainMenuSounds.ExitButtonSound);
    }
    public void StayButton()
    {
        PanelSelecter(mainMenuPanels.MainMenuPanel);
        audioSource.PlayOneShot(mainMenuSounds.ButtonSound);
    }
    public void LeaveButton()
    {
        audioSource.PlayOneShot(mainMenuSounds.ExitButtonSound);
        Application.Quit();     
    }
    public void BackButton()
    {
        PanelSelecter(mainMenuPanels.MainMenuPanel);
        audioSource.PlayOneShot(mainMenuSounds.ButtonSound);
    }
    public void VolumeSlider()
    {
        audioSource.volume = mainMenuSounds.VolumeSlider.value;
        mainMenuSounds.menuMusic.volume = mainMenuSounds.VolumeSlider.value;
        PlayerDataManager.SetVolume(mainMenuSounds.VolumeSlider.value);
    }
    public void SoundToggle()
    {
        audioSource.mute = mainMenuSounds.SoundToggle.isOn;
        mainMenuSounds.menuMusic.mute = mainMenuSounds.SoundToggle.isOn;
        mainMenuSounds.MusicToggle.isOn = mainMenuSounds.SoundToggle.isOn;
        PlayerDataManager.SetSound(mainMenuSounds.SoundToggle.isOn);
        PlayerDataManager.SetMusic(mainMenuSounds.MusicToggle.isOn);
    }
    public void MusicToggle()
    {
        if(!audioSource.mute)
        {
            mainMenuSounds.menuMusic.mute = mainMenuSounds.MusicToggle.isOn;
            PlayerDataManager.SetMusic(mainMenuSounds.MusicToggle.isOn);
        }
    }
}
[Serializable]
public struct MainMenuPanels
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject NoAdsPanel;
    public GameObject ExitQuaryPanel;

    public GameObject PlayPanel;
    public GameObject LevelSelectPanel;
    public GameObject LevelSelectBackPanel;
}
[Serializable]
public struct MainMenuSounds
{
    public Slider VolumeSlider;
    public Toggle SoundToggle;
    public Toggle MusicToggle;

    public AudioClip ButtonSound;
    public AudioClip ExitButtonSound;

    public AudioSource menuMusic;
}


