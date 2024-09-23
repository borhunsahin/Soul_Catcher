using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    private GameManager gameManager;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void LateUpdate()
    {
        AgentController();
    }
    private void AgentController()
    {
        if (!gameManager.isFight)
        {
            navMeshAgent.SetDestination(gameManager.followPoint.transform.position);
            gameManager.transform.rotation = Quaternion.LookRotation(gameManager.followPoint.transform.position);
        }

        else
        {
            navMeshAgent.SetDestination(gameManager.fightPoint.transform.position);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            gameManager.Particle(gameManager.penaltyParticleList,transform.position);
            gameObject.SetActive(false);
            gameManager.agentCount--;
        }
        else if(other.CompareTag("EnemyAgent"))
        {
            gameManager.Particle(gameManager.agentParticleList, transform.position);
            gameManager.Particle(gameManager.penaltyParticleList, other.transform.position);
            gameObject.SetActive(false);
            gameManager.agentCount--;
            other.gameObject.SetActive(false);
            gameManager.enemyAgentCount--;
        }
    }
}
