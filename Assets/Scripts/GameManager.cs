using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
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
    public bool GameOver = false;

    public int agentCount = 0;
    public int enemyAgentCount = 0;

    public Slider slider;

    void Start()
    {
        enemyAgentCount = fightPlatform.enemyAgentCount;

        slider.maxValue = fightPlatform.transform.position.z;
    }

    void Update()
    {       
        if(Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseAgent();
        }
        if (isFight)
        {
            if(agentCount == 0)
                GameOver = true;
            else if(enemyAgentCount == 0)
                isFight = false;
        }

        slider.value = player.transform.position.z;
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
    public void Fight(bool status)
    {
        isFight = status;

    }
    public void EndFight(bool status)
    {
        isFight = status;
    }
}