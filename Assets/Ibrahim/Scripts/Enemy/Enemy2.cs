using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour, IPokeble
{
    int health = 1;
    Animator anim;
    Player_Movement cart;
    NavMeshAgent agent;
    [SerializeField] public float speed;
    [SerializeField] private Rigidbody ninjaStar;
    [SerializeField] private Transform spawn;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        cart = FindObjectOfType<Player_Movement>();
        agent.speed = agent.speed + Random.Range(-.5f, .5f);
    }

    void Update()
    {

        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        string currentStateName = "test";
        if (clipInfo.Length > 0)
        {
            currentStateName = clipInfo[0].clip.name;
            Debug.Log("Current Animation State: " + currentStateName);
        }
        if (agent.velocity != Vector3.zero) anim.Play("Run");
        else
        {
            transform.LookAt(cart.transform);
            if (currentStateName != "Idle" && currentStateName != "Throw") anim.Play("Idle");
        }

        agent.SetDestination(cart.transform.position);
    }

    public void Throw()
    {
        Vector3 pos = new Vector3(cart.transform.position.x, 0, cart.transform.position.z);
        Instantiate(ninjaStar, spawn.position, Quaternion.identity).AddForce((pos - transform.position).normalized * speed, ForceMode.Force);
    }

    public void OnPoke(Vector3 impulse)
    {
        health--;
        if (health <= 0) Destroy(gameObject);
    }
}
