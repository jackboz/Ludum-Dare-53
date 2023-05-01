using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Crate cart;
    Animator anim;
    Enemy_Spawner spawner;
    NavMeshAgent agent;
    [SerializeField] private GameObject smoke;

    void Start()
    {
        spawner = FindObjectOfType<Enemy_Spawner>();
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Crate>();
        anim = GetComponent<Animator>();
        agent.speed += Random.Range(-1.0f, 1.0f);
    }

    private void FixedUpdate()
    {
        if (transform.position.z < cart.transform.position.z - 30) { Destroy(gameObject); }
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
}