using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPokeble
{
    int health = 1;
    Cart_Stuff cart;
    NavMeshAgent agent;
    [SerializeField] private Transform stuffDestination;
    [HideInInspector] public bool hasStuff;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Cart_Stuff>();
    }

    private void FixedUpdate()
    {
        cart = FindObjectOfType<Cart_Stuff>();
        agent.SetDestination(cart.transform.position);

        if(Vector3.Distance(cart.transform.position, transform.position) < 1.5f && !hasStuff)
        {
            cart.stuff -= 1;
            hasStuff = true;
        }
        if(hasStuff) agent.SetDestination(stuffDestination.position);
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        if (health <= 0)
        {
            cart.stuff += 1;
            Destroy(gameObject);
        }
    }
}
