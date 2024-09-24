using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] ButtonList;
    void Start()
    {
        PlayerDataManager.SetLastLevel(4);
        PlayerPrefs.Save();

        for (int i = 0; i < PlayerDataManager.GetLastLevel(); i++)
        {
            int buttonIndex = i + 1;
            ButtonList[i].GetComponent<Button>().interactable = true;
            ButtonList[i].GetComponent<Button>().onClick.AddListener(delegate { GetButtonIndex(buttonIndex); });

        }

    }
    public void GetButtonIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}


