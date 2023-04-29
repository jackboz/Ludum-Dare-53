using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 input;
    bool isDashing;
    bool cooldown;
    Player_Poke poke;
    [SerializeField] private float speed;

    private void Start()
    {
        poke = GetComponent<Player_Poke>();
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("OnMove");
        Vector2 temp = value.Get<Vector2>();
        if (!isDashing) input = new Vector3(temp.x, 0f, temp.y);
        Debug.Log(input);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !cooldown) { StartCoroutine(dash(.2f)); }
        Vector3 direction = (transform.position + input) - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }

    IEnumerator dash(float time)
    {
        isDashing = true;
        cooldown = true;
        yield return new WaitForSeconds(time);
        isDashing = false;
        yield return new WaitForSeconds(1.25f);
        cooldown = false;
    }

    private void FixedUpdate()
    {
        rb.velocity = speed * input * (isDashing ? 3 : 1) * (poke.hasCrate? .7f:1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !cooldown)
        {
            input = Vector3.zero;
            StartCoroutine(dash(1));
            input = Vector3.zero;
        }
    }
}
