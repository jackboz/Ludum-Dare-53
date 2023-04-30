using UnityEngine;
using UnityEngine.AI;

public class Ranged : MonoBehaviour, IPokeble
{
    int health = 1;
    float timer;
    Player_Poke cart;
    NavMeshAgent agent;
    [HideInInspector] public bool hasTheGoods;
    [SerializeField] public float speed;
    [SerializeField] private Rigidbody ninjaStar;
    [SerializeField] private Transform spawn;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Player_Poke>();
        agent.speed = agent.speed + Random.Range(-.5f, .5f);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Vector3 pos = new Vector3(cart.transform.position.x, 0, cart.transform.position.z);
            Instantiate(ninjaStar, spawn.position, Quaternion.identity).AddForce((pos - transform.position).normalized * speed, ForceMode.Force);
            timer = 2;
        }
        agent.SetDestination(cart.transform.position);
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        if (health <= 0) Destroy(gameObject);
    }
}