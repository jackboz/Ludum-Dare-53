using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour, IPokeble
{
    int health = 1;
    Player_Movement cart;
    NavMeshAgent agent;
    [HideInInspector] public bool hasTheGoods;
    [SerializeField] public float speed;
    [SerializeField] private Rigidbody ninjaStar;
    [SerializeField] private Transform spawn;
    private SoundManager soundManager;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Player_Movement>();
        agent.speed = agent.speed + Random.Range(-.5f, .5f);
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        string currentStateName = "test";
        if (clipInfo.Length > 0)
        {
            currentStateName = clipInfo[0].clip.name;
            Debug.Log("Current Animation State: " + currentStateName);
        }
        if (agent.velocity != Vector3.zero) animator.Play("Run");
        else
        {
            transform.LookAt(cart.transform);
            if (currentStateName != "Idle" && currentStateName != "Throw") animator.Play("Idle");
        }
        agent.SetDestination(cart.transform.position);
    }

    public void Throw()
    {
        //soundManager.PlayShurikenThrowSound();
        Vector3 pos = new Vector3(cart.transform.position.x, 0, cart.transform.position.z);
        Instantiate(ninjaStar, spawn.position, Quaternion.identity).AddForce((pos - transform.position).normalized * speed, ForceMode.Force);
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        if (health <= 0) Destroy(gameObject);
    }
}
