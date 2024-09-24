using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Panels panel;
    public Sounds sounds;
    private List<GameObject> panelList;

    AudioSource audioSource;
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
    }
    private void LoadScence(int index)
    {
        SceneManager.LoadScene(index);
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
        audioSource.PlayOneShot(sounds.ButtonSound);
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
    public AudioClip ButtonSound;
    public AudioClip ExitButtonSound;
}

