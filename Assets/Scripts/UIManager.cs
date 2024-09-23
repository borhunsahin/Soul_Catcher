using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Panels panel;
    private List<GameObject> panelList;
    private void Start()
    {
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

    }
    public void PlayButton()
    {
        PanelSelecter(panel.PlayPanel);
    }
    public void LevelSelecterButton()
    {
        PanelSelecter(panel.LevelSelectPanel);
    }
    public void SettingsButton()
    {
        PanelSelecter(panel.SettingsPanel);
    }
    public void NoADSButton()
    {
        PanelSelecter(panel.NoAdsPanel);
    }
    public void ExitButton()
    {
        PanelSelecter(panel.ExitQuaryPanel);
    }
    public void BackButton()
    {
        PanelSelecter(panel.MainMenuPanel);
    }
    public void StayButton()
    {
        PanelSelecter(panel.MainMenuPanel);
    }
    public void LeaveButton()
    {
        Application.Quit();
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
