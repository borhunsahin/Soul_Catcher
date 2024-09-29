using UnityEngine;
using UnityEngine.AI;

public class Enemy_Agent : MonoBehaviour
{
    GameManager gameManager;
    private NavMeshAgent navMeshAgent;
    private Animator enemyAnimator;

    [SerializeField] private GameObject fightPoint;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        if(gameManager.isFight && !gameManager.isGameOver)
        {
            navMeshAgent.SetDestination(fightPoint.transform.position);
        }
        enemyAnimator.SetBool("isFight", gameManager.isFight && !gameManager.isGameOver);
    }
}
