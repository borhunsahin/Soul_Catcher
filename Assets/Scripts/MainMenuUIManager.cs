using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public Panels panel;
    private List<GameObject> panelList;

    private AudioSource audioSource;
    public Sounds sounds;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        panelList = new List<GameObject>
        {
            panel.MainMenuPanel,
            panel.SettingsPanel,
            panel.NoAdsPanel,
            panel.ExitQuaryPanel,
            panel.PlayPanel,
            panel.LevelSelectPanel,
            panel.LevelSelectBackPanel
        };

        PlayerDataManager.CheckKey();

        sounds.VolumeSlider.value = PlayerDataManager.GetVolume();
        sounds.SoundToggle.isOn = PlayerDataManager.GetSound();
        sounds.MusicToggle.isOn = PlayerDataManager.GetMusic();
    }

    private void PanelSelecter(GameObject panelName) 
    {
        
        foreach (GameObject p in panelList)
        {
            if(p.name == panelName.name)
            {
                p.SetActive(true);
            }
            else
            {
                if(p.name != panel.MainMenuPanel.name)
                    p.SetActive(false);
            }
        }
    }
    public void ContinueButton()
    {
        SceneManager.LoadScene(PlayerDataManager.GetLastLevel());
        
    }
    public void PlayButton()
    {
        PanelSelecter(panel.PlayPanel);
        audioSource.PlayOneShot(sounds.ButtonSound);
    }
    public void SandBoxButton()
    {

        audioSource.PlayOneShot(sounds.ButtonSound);
    }
    public void LevelSelecterButton()
    {
        PanelSelecter(panel.LevelSelectPanel);
        audioSource.PlayOneShot(sounds.ButtonSound);
    }
    public void SettingsButton()
    {
        PanelSelecter(panel.SettingsPanel);
        audioSource.PlayOneShot(sounds.ButtonSound);
    }
    public void NoADSButton()
    {
        PanelSelecter(panel.NoAdsPanel);
        audioSource.PlayOneShot(sounds.ButtonSound);
    }
    public void ExitButton()
    {
        PanelSelecter(panel.ExitQuaryPanel);
        audioSource.PlayOneShot(sounds.ExitButtonSound);
    }
    public void BackButton()
    {
        PanelSelecter(panel.MainMenuPanel);
        audioSource.PlayOneShot(sounds.ButtonSound);
    }
    public void StayButton()
    {
        PanelSelecter(panel.MainMenuPanel);
        audioSource.PlayOneShot(sounds.ButtonSound);
    }
    public void LeaveButton()
    {
        Application.Quit();
        audioSource.PlayOneShot(sounds.ExitButtonSound);
    }
    public void VolumeSlider()
    {
        audioSource.volume = sounds.VolumeSlider.value;
        sounds.menuMusic.volume = sounds.VolumeSlider.value;
        PlayerDataManager.SetVolume(sounds.VolumeSlider.value);
    }
    public void SoundToggle()
    {
        audioSource.mute = sounds.SoundToggle.isOn;
        sounds.menuMusic.mute = sounds.SoundToggle.isOn;
        sounds.MusicToggle.isOn = sounds.SoundToggle.isOn;
        PlayerDataManager.SetSound(sounds.SoundToggle.isOn);
        PlayerDataManager.SetMusic(sounds.MusicToggle.isOn);
    }
    public void MusicToggle()
    {
        if(!audioSource.mute)
        {
            sounds.menuMusic.mute = sounds.MusicToggle.isOn;
            PlayerDataManager.SetMusic(sounds.MusicToggle.isOn);
        }
    }
}
[Serializable]
public struct Panels
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
public struct Sounds
{
    public Slider VolumeSlider;
    public Toggle SoundToggle;
    public Toggle MusicToggle;

    public AudioClip ButtonSound;
    public AudioClip ExitButtonSound;

    public AudioSource menuMusic;
}


