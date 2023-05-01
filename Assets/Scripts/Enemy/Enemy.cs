using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Crate crate;
    Animator anim;
    Enemy_Spawner spawner;
    NavMeshAgent agent;
    [SerializeField] private GameObject smoke;

    void Awake()
    {
        spawner = FindObjectOfType<Enemy_Spawner>();
        agent = GetComponent<NavMeshAgent>();
        crate = FindObjectOfType<Crate>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        agent.speed += Random.Range(-.5f, 1.0f);
    }

    private void FixedUpdate()
    {
        if (transform.position.z < crate.transform.position.z - 20) { Destroy(gameObject); }
    }

    private void OnDestroy()
    {
        Instantiate(smoke, transform.position, Quaternion.identity);
        spawner.enemyList.Remove(this);
    }

    public void OnDeath()
    {
        Destroy(GetComponent<Enemy1>());
        Destroy(GetComponent<Enemy2>());
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<NavMeshAgent>());
        anim.Play("Death");
    }

    public void Des()
    {
        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }
}