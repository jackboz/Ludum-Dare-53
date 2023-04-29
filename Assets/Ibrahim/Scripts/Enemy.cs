using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPokeble
{
    int health = 1;
    Cart cart;
    NavMeshAgent agent;
    [SerializeField] private Transform stuffDestination;
    [HideInInspector] public bool hasStuff;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Cart>();
    }

    private void FixedUpdate()
    {
        agent.SetDestination(cart.transform.position);

        if(Vector3.Distance(cart.transform.position, transform.position) < 3 && !hasStuff)
        {
            cart.goodsAmount-= 1;
            hasStuff = true;
        }
        if(hasStuff) agent.SetDestination(stuffDestination.position);
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        if (health <= 0)
        {
            cart.goodsAmount += 1;
            Destroy(gameObject);
        }
    }
}
