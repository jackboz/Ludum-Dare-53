using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour, IPokeble
{
    private Crate cart;
    private NavMeshAgent agent;
    private SoundManager soundManager;
    private Enemy enemy;

    private int health = 1;
    float randomXEscape;
    private bool hasTheGoods;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        randomXEscape = Random.Range(-9.0f, 9.0f);
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Crate>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {

        if (hasTheGoods) agent.SetDestination(new Vector3(randomXEscape, 1, transform.position.z - 20));
        else
        {
            agent.SetDestination(cart.transform.position);

            if (Vector3.Distance(cart.transform.position, transform.position) < 3)
            {
                if (cart.goodsAmount > 0)
                {
                    cart.StealGoods();
                    hasTheGoods = true;
                    soundManager.PlayBunTakenSound();
                }
                else Destroy(gameObject);
            }
        }
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        if (health <= 0)
        {
            if (hasTheGoods) cart.ReturnGoods(1);
            soundManager.PlayEnemyDeathSounds();
            impulse.y = 0;
            enemy.SetDyingVector(impulse);
            enemy.OnDeath();
        }
    }
}
