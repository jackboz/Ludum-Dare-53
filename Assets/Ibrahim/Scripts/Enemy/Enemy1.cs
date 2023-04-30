using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour, IPokeble
{
    int health = 1;
    Crate cart;
    NavMeshAgent agent;
    MeshRenderer mesh;
    [HideInInspector] public bool hasTheGoods;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Crate>();
        agent.speed = agent.speed + Random.Range(-.5f, .5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (hasTheGoods)
        {
            agent.SetDestination(new Vector3(0, 1, transform.position.z - 20));
        }
        else
        {
            agent.SetDestination(cart.transform.position);

            if (Vector3.Distance(cart.transform.position, transform.position) < 3)
            {
                //oD Wrecjz added this to make sure the enemy cant steal non existant buns
                if (cart.goodsAmount > 0)
                {
                    cart.StealGoods();
                    hasTheGoods = true;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
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
