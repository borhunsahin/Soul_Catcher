using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyAgentList;
    [SerializeField] public int enemyAgentCount;
    void Start()
    {
        SetEnemyAgentFighters();
    }
    void Update()
    {
        
    }
    private void SetEnemyAgentFighters()
    {
        for (int i = 0; i < enemyAgentCount; i++)
        {
            foreach (var agent in enemyAgentList)
            {
                if (!agent.activeInHierarchy)
                {
                    agent.SetActive(true);
                    break;
                }
            }
        }
    }
}
