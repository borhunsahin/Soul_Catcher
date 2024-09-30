using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    MainMenuUIManager mainMenuUIManager;
    [SerializeField] GameObject[] ButtonList;

    public GameObject LoadingPanel;
    public Slider LoadingSlider;
    void Start()
    {
        mainMenuUIManager = GameObject.Find("UIManager").GetComponent<MainMenuUIManager>();

        for (int i = 0; i < PlayerDataManager.GetLastLevel(); i++)
        {
            int buttonIndex = i + 1;
            ButtonList[i].GetComponent<Button>().interactable = true;
            ButtonList[i].GetComponent<Button>().onClick.AddListener(delegate { GetButtonIndex(buttonIndex); });
        }
    }
    public void GetButtonIndex(int index)
    {
        //SceneManager.LoadScene(index);
        StartCoroutine(LoadAsync(index));
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation aSyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        mainMenuUIManager.mainMenuPanels.loadingPanel.SetActive(true);

        while (!aSyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(aSyncOperation.progress / 0.9f);
            mainMenuUIManager.mainMenuPanels.loadingSlider.value = progress;
            yield return null;
        }
    }
}


