using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour, IPokeble
{
    int health = 1;
    Player_Poke cart;
    Enemy_Spawner spawner;
    NavMeshAgent agent;
    MeshRenderer mesh;
    [HideInInspector] public bool hasTheGoods;
    [SerializeField] public float speed;
    [SerializeField] private Rigidbody ninjaStar;
    [SerializeField] private Transform spawn;

    void Start()
    {

        spawner = FindObjectOfType<Enemy_Spawner>();
        mesh = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Player_Poke>();
        agent.speed = agent.speed + Random.Range(-.5f, .5f);
    }

    float timer;

    // Update is called once per frame
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
