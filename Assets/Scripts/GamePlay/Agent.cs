using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    private GameManager gameManager;
    private NavMeshAgent navMeshAgent;
    private Animator agentAnimator;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        agentAnimator = GetComponent<Animator>(); 
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
        else if(gameManager.isFight)
        {
            navMeshAgent.SetDestination(gameManager.fightPoint.transform.position);
        }
        else if(gameManager.isWin)
        {
            navMeshAgent.speed = 0;
        }

        agentAnimator.SetBool("isWin", gameManager.isWin);
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
