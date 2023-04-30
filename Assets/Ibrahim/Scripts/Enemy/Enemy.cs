using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Crate cart;
    Animator anim;
    Enemy_Spawner spawner;
    NavMeshAgent agent;
    bool isDead;

    void Start()
    {
        spawner = FindObjectOfType<Enemy_Spawner>();
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Crate>();
        anim = GetComponent<Animator>();
        agent.speed = agent.speed + Random.Range(-.5f, .5f);
    }

    private void FixedUpdate()
    {
        if (transform.position.z < cart.transform.position.z - 30) { Destroy(gameObject); }
    }

    private void OnDestroy()
    {
        spawner.enemyList.Remove(this);
    }

    public void OnDeath()
    {
        Destroy(GetComponent<Enemy1>());
        Destroy(GetComponent<Enemy2>());
        Destroy(GetComponent<NavMeshAgent>());
        anim.Play("Death");
    }

    public void Des()
    {
        Destroy(gameObject);
    }
}