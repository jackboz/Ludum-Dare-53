using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Crate crate;
    Animator anim;
    Enemy_Spawner spawner;
    NavMeshAgent agent;
    [SerializeField] private GameObject smoke;

    void Start()
    {
        spawner = FindObjectOfType<Enemy_Spawner>();
        agent = GetComponent<NavMeshAgent>();
        crate = FindObjectOfType<Crate>();
        anim = GetComponent<Animator>();
        agent.speed += Random.Range(-1.0f, 1.0f);
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
        gameObject.layer = 9;
        Destroy(GetComponent<NavMeshAgent>());
        Rigidbody rb =  gameObject.AddComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.AddForce(transform.forward * -1 * 3, ForceMode.Impulse);
        anim.Play("Death");
    }

    public void Des()
    {
        Destroy(gameObject);
    }
}