using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour, IPokeble
{
    private Player_Movement player;
    private NavMeshAgent agent;
    private SoundManager soundManager;
    private Animator animator;
    private Enemy enemy;
    private Enemy_Spawner spawner;

    private int health = 1;

    [SerializeField] public float throwSpeed;
    [SerializeField] private Rigidbody ninjaStar;
    [SerializeField] private Transform spawn;


    void Awake()
    {
        spawner = FindObjectOfType<Enemy_Spawner>();
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player_Movement>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        int howManyBlack = 0;
        for(int i =  0; i < spawner.enemyList.Count; i++)
        {
            if (spawner.enemyList[i].GetComponent<Enemy1>()) howManyBlack++;
        }
        if (howManyBlack == 0) Destroy(gameObject); 

        if (agent.velocity != Vector3.zero) animator.Play("Run");
        else
        {
            transform.LookAt(player.transform);
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

            if (clipInfo.Length > 0)
            {
                string currentStateName = clipInfo[0].clip.name;
                if (currentStateName != "Idle" && currentStateName != "Throw") animator.Play("Idle");
            }
        }
        agent.SetDestination(player.transform.position);
    }

    public void Throw()
    {
        soundManager.PlayShurikenThrowSound();
        Vector3 pos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Instantiate(ninjaStar, spawn.position, Quaternion.Euler(270, 0, 0)).AddForce((pos - transform.position).normalized * throwSpeed, ForceMode.Force);
        soundManager.PlayShurikenThrowSound();
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        soundManager.PlayEnemyDeathSounds();
        impulse.y = 0;
        enemy.SetDyingVector(impulse);
        if (health <= 0) enemy.OnDeath();
    }
}
