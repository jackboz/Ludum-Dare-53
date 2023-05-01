using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour, IPokeble
{
    private Player_Movement cart;
    private NavMeshAgent agent;
    private SoundManager soundManager;
    private Animator animator;
    Enemy enemy;

    private int health = 1;

    [SerializeField] public float speed;
    [SerializeField] private Rigidbody ninjaStar;
    [SerializeField] private Transform spawn;


    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Player_Movement>();
        agent.speed += Random.Range(-.5f, .5f);
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        string currentStateName = "test";
        if (clipInfo.Length > 0)
        {
            currentStateName = clipInfo[0].clip.name;
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
        soundManager.PlayShurikenThrowSound();
        Vector3 pos = new Vector3(cart.transform.position.x, 0, cart.transform.position.z);
        Instantiate(ninjaStar, spawn.position, Quaternion.Euler(270, 0, 0)).AddForce((pos - transform.position).normalized * speed, ForceMode.Force);
        soundManager.PlayShurikenThrowSound();
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        soundManager.PlayEnemyDeathSounds();
        if (health <= 0) enemy.OnDeath();
    }
}
