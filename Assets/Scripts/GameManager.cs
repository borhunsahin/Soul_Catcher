using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameUIManager gameUIManager;

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject spawnPoint;
    [SerializeField] public GameObject followPoint;
    [SerializeField] public GameObject fightPoint;

    [SerializeField] private GameObject[] agentList;
    

    [SerializeField] private GameObject[] spawnParticleList;
    [SerializeField] public GameObject[] penaltyParticleList;
    [SerializeField] public GameObject[] agentParticleList;
    [SerializeField] public GameObject[] freeAgentParticleList;

    [SerializeField] private FightPlatform fightPlatform;

    public bool isFight = false;
    public bool isGameOver = false;

    public int agentCount = 0;
    public int enemyAgentCount = 0;

    public Slider slider;

    void Start()
    {
        gameUIManager = GameObject.Find("UIManager").GetComponent<GameUIManager>();

        Time.timeScale = 1; // Tab to continue butonu ile bir fonksiyon yap
        enemyAgentCount = fightPlatform.enemyAgentCount;

        slider.maxValue = fightPlatform.transform.position.z;
    }

    void Update()
    {       
        if(Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseAgent();
        }

        Fight();

        slider.value = player.transform.position.z; // Mesafe Slider ı gamemanager den buraya taşı
    }
    public void IncreaseAgent()
    {
        foreach (var agent in agentList)
        {
            if (!agent.activeInHierarchy)
            {
                agent.transform.position = spawnPoint.transform.position;
                agent.SetActive(true);
                Particle(spawnParticleList, followPoint.transform.position);
                agentCount++;
                break;
            }
        }
    }
    private void DecreaseAgent()
    {
        foreach (var agent in agentList)
        {
            if (agent.activeInHierarchy)
            {
                Particle(penaltyParticleList, player.transform.position);
                agent.SetActive(false);
                agentCount--;
                break;
            }
        }
    }

    public void AgentProcess(string process,int number)
    {
        switch (process)
        {
            case "+":
                for (int i = 0; i < number; i++)
                {
                    IncreaseAgent();
                }
                break;

            case "-":
                for (int i = 0; i < number; i++)
                {
                    DecreaseAgent();
                }
                break;

            case "x":
                int multiplyCount = (agentCount * number) - agentCount;
                if(number!=0)
                {
                    for (int i = 0; i < multiplyCount; i++)
                    {
                        IncreaseAgent();
                    }
                }
                else
                {
                    for (int i = 0; i < Mathf.Abs(multiplyCount); i++)
                    {
                        DecreaseAgent();
                    }
                }
                break;

            case "/":
                int divideCount = agentCount - (agentCount / number);

                if (number != 0)
                {
                    for (int i = 0; i < divideCount; i++)
                    {
                        DecreaseAgent();
                    }
                }   
                break;

            case "^":
                int loop3 = (agentCount * agentCount) - agentCount;
                for (int i = 0; i < loop3; i++)
                {
                    IncreaseAgent();
                }
                break;

            case "√":
                int loop4 = agentCount - (int)Mathf.Sqrt(agentCount);
                Debug.Log(loop4);
                for (int i = 0; i < loop4; i++)
                {
                    DecreaseAgent();
                }
                break;

        }    
    }
    public void Particle(GameObject[] particleList,Vector3 agentPosition)
    {
        foreach (var particle in particleList)
        {
            if (!particle.activeInHierarchy)
            {
                particle.transform.position = new Vector3(agentPosition.x,.25f, agentPosition.z);
                particle.SetActive(true);
                if (particle.GetComponent<AudioSource>() != null)
                    particle.GetComponent<AudioSource>().Play();
                break;
            } 
        }
    }
    public void Fight()
    {
        if (isFight)
        {
            if (enemyAgentCount == 0)
            {
                gameUIManager.EndGameProcess(isGameOver);

                if(SceneManager.GetActiveScene().buildIndex == PlayerDataManager.GetLastLevel())
                    PlayerDataManager.SetLastLevel(PlayerDataManager.GetLastLevel() + 1);
                
            }   
            else if (enemyAgentCount > 0 && agentCount == 0)
            {
                isGameOver = true;
                gameUIManager.EndGameProcess(isGameOver);
            }
                
        }
    }
}