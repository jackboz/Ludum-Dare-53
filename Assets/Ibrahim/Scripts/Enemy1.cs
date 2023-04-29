using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour, IPokeble
{
    int health = 1;
    Crate cart;
    Enemy_Spawner spawner;
    NavMeshAgent agent;
    MeshRenderer mesh;
    [HideInInspector] public bool hasTheGoods;
    [SerializeField] public Material hasGoodsMat;

    void Start()
    {

        spawner = FindObjectOfType<Enemy_Spawner>();
        mesh = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Crate>();
        agent.speed = agent.speed + Random.Range(-.5f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(cart.transform.position, transform.position) < 3 && !hasTheGoods)
        {
            cart.StealGoods();
            hasTheGoods = true;
        }
        if (hasTheGoods)
        {
            mesh.material = hasGoodsMat;
            agent.SetDestination(new Vector3(0, 1, transform.position.z - 50));
        }
        else agent.SetDestination(cart.transform.position);
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        if (health <= 0)
        {
            if (hasTheGoods) cart.ReturnGoods(1);
            Destroy(gameObject);
        }
    }
}
