using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Crate cart;
    private Vector3 inputTemp;
    private Vector3 input;
    private bool isDashing;
    private bool cooldown;
    [SerializeField] private float returnDistance = 3f;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float speed;
    [SerializeField] private Transform spherePos;
    [SerializeField] private Transform cratePos;
    [SerializeField] private bool isCoolingDown;
    [SerializeField] private bool hasCrate;

    private void Start()
    {
        cart = FindObjectOfType<Crate>();
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        if (!isDashing) input = inputTemp;
        rb.velocity = speed * input * (isDashing ? 3 : 1) * (hasCrate ? .5f : 1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !cooldown && !hasCrate) { StartCoroutine(dash(.2f)); }
        Vector3 direction = (transform.position + input) - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
        if (Vector3.Distance(transform.position, cart.transform.position) < returnDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hasCrate)
                {
                    cart.Drop();
                    cart.transform.parent = null;
                }
                else
                {
                    cart.PickUp();
                    cart.transform.position = cratePos.position;
                    cart.transform.parent = cratePos;
                }
                hasCrate = !hasCrate;
            }
        }
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

    public void OnMove(InputValue value)
    {
        Debug.Log("OnMove");
        Vector2 temp = value.Get<Vector2>();
        inputTemp = new Vector3(temp.x, 0f, temp.y);
    }

    public void OnPoke()
    {
        Debug.Log("OnPokeInput");
        if (hasCrate || isCoolingDown) return;
        int layerMask = 1 << 6;
        Collider[] coll = Physics.OverlapSphere(spherePos.position, sphereRadius, layerMask);
        if (coll.Length == 0) return;
        foreach (Collider collider in coll) collider.GetComponent<IPokeble>()?.OnPoke(transform.forward);
        StartCoroutine(pokeCooldown());
    }

    IEnumerator dash(float time)
    {
        isDashing = true;
        cooldown = true;
        yield return new WaitForSeconds(time);
        isDashing = false;
        yield return new WaitForSeconds(1f);
        cooldown = false;
    }

    IEnumerator pokeCooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
    }
}