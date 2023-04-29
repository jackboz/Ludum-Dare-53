using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPokeble
{
    int health = 1;
    Cart cart;
    Inventory _playerInventory;
    NavMeshAgent agent;
    [SerializeField] private Transform stuffDestination;
    [HideInInspector] public bool hasStuff;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Cart>();
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void FixedUpdate()
    {
        agent.SetDestination(cart.transform.position);

        if(Vector3.Distance(cart.transform.position, transform.position) < 3 && !hasStuff)
        {
            cart.StealGoods();
            hasStuff = true;
        }
        if(hasStuff) agent.SetDestination(stuffDestination.position);
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        if (health <= 0)
        {
            if (hasStuff) _playerInventory.AddGoods();
            Destroy(gameObject);
        }
    }
}
