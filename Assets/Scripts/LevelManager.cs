using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] ButtonList;

    public GameObject LoadingPanel;
    public Slider LoadingSlider;
    void Start()
    {
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


