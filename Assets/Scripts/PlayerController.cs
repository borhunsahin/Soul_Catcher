using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    [HideInInspector] public Animator playerAnimator;

    [SerializeField] private float playerSpeed;
    public bool isHold = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Controller();
    }
    private void Controller()
    {
        if(Input.GetKey(KeyCode.Space) && !gameManager.isFight)
            isHold = true;
        else
            isHold = false;

        if (!gameManager.isFight && !isHold)
        {
            transform.Translate(transform.forward * Time.deltaTime * playerSpeed);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0 && transform.position.x > -4)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);

                if (Input.GetAxis("Mouse X") > 0 && transform.position.x < 4)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
            }
        }


        playerAnimator.SetBool("isFight", gameManager.isFight || isHold);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Buff") || other.CompareTag("DeBuff"))
        {
            string[] buff = (other.gameObject.GetComponentInChildren<TextMeshPro>().text).Split();
            gameManager.AgentProcess(buff[0], int.Parse(buff[1]));
        }
        if (other.CompareTag("FreeAgent"))
        {
            gameManager.Particle(gameManager.freeAgentParticleList,gameManager.player.transform.position);
            gameManager.IncreaseAgent();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("FightPlatform"))
        {
            gameManager.isFight= true;
        }

    }
    public Vector3 GetPosition() => transform.position;
}
