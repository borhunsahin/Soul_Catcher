using System;
using System.Collections;
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
            mainMenuPanels.mainMenuPanel,
            mainMenuPanels.settingsPanel,
            mainMenuPanels.noAdsPanel,
            mainMenuPanels.exitQuaryPanel,
            mainMenuPanels.playPanel,
            mainMenuPanels.levelSelectPanel,
            mainMenuPanels.levelSelectBackPanel
        };

        PlayerDataManager.CheckKey();

        mainMenuSounds.volumeSlider.value = PlayerDataManager.GetVolume();
        mainMenuSounds.soundToggle.isOn = PlayerDataManager.GetSound();
        mainMenuSounds.musicToggle.isOn = PlayerDataManager.GetMusic();
    }

    private void PanelSelecter(GameObject panelName) 
    {
        
        foreach (GameObject panel in panelList)
        {
            if(panel.name == panelName.name)
            {
                panel.SetActive(true);

                if (!panelName == mainMenuPanels.exitQuaryPanel)
                    audioSource.PlayOneShot(mainMenuSounds.buttonSound);
                else
                    audioSource.PlayOneShot(mainMenuSounds.exitButtonSound);
            }
            else
            {
                if(panel.name != mainMenuPanels.mainMenuPanel.name)
                {
                    audioSource.PlayOneShot(mainMenuSounds.buttonSound);
                    panel.SetActive(false);
                }     
            }
        }
    }
    public void ContinueButton()
    {
        audioSource.PlayOneShot(mainMenuSounds.buttonSound);
        //SceneManager.LoadScene(PlayerDataManager.GetLastLevel());
        StartCoroutine(LoadAsync(PlayerDataManager.GetLastLevel()));
        
    }
    public void PlayButton()
    {
        PanelSelecter(mainMenuPanels.playPanel);
    }
    public void SandBoxButton()
    {
        ////////////////////////////////////////////////////
    }
    public void LevelSelecterButton()
    {
        PanelSelecter(mainMenuPanels.levelSelectPanel);   
    }
    public void SettingsButton()
    {
        PanelSelecter(mainMenuPanels.settingsPanel);
    }
    public void NoADSButton()
    {
        PanelSelecter(mainMenuPanels.noAdsPanel);
    }
    public void ExitButton()
    {
        PanelSelecter(mainMenuPanels.exitQuaryPanel);  
    }
    public void StayButton()
    {
        PanelSelecter(mainMenuPanels.mainMenuPanel);  
    }
    public void LeaveButton()
    {
        Application.Quit();     
    }
    public void BackButton()
    {
        PanelSelecter(mainMenuPanels.mainMenuPanel);
    }
    public void VolumeSlider()
    {
        audioSource.volume = mainMenuSounds.volumeSlider.value;
        mainMenuSounds.menuMusic.volume = mainMenuSounds.volumeSlider.value;
        PlayerDataManager.SetVolume(mainMenuSounds.volumeSlider.value);
    }
    public void SoundToggle()
    {
        audioSource.mute = mainMenuSounds.soundToggle.isOn;
        mainMenuSounds.menuMusic.mute = mainMenuSounds.soundToggle.isOn;
        mainMenuSounds.musicToggle.isOn = mainMenuSounds.soundToggle.isOn;
        PlayerDataManager.SetSound(mainMenuSounds.soundToggle.isOn);
        PlayerDataManager.SetMusic(mainMenuSounds.musicToggle.isOn);
    }
    public void MusicToggle()
    {
        if(!audioSource.mute)
        {
            mainMenuSounds.menuMusic.mute = mainMenuSounds.musicToggle.isOn;
            PlayerDataManager.SetMusic(mainMenuSounds.musicToggle.isOn);
        }
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation aSyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        mainMenuPanels.loadingPanel.SetActive(true);

        while(!aSyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(aSyncOperation.progress / 0.9f);
            mainMenuPanels.loadingSlider.value = progress;
            yield return null;
        }
    }
}
[Serializable]
public struct MainMenuPanels
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject noAdsPanel;
    public GameObject exitQuaryPanel;

    public GameObject playPanel;
    public GameObject levelSelectPanel;
    public GameObject levelSelectBackPanel;

    public GameObject loadingPanel;
    public Slider loadingSlider;
}
[Serializable]
public struct MainMenuSounds
{
    public Slider volumeSlider;
    public Toggle soundToggle;
    public Toggle musicToggle;

    public AudioClip buttonSound;
    public AudioClip exitButtonSound;

    public AudioSource menuMusic;
}


