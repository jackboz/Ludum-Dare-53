using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Crate cart;
    Enemy_Spawner spawner;
    NavMeshAgent agent;

    void Start()
    {

        spawner = FindObjectOfType<Enemy_Spawner>();
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Crate>();
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
}
