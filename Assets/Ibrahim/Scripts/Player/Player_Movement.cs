using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Crate cart;
    Animator animator;

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
    [SerializeField] private GameObject camPos;
    [SerializeField] private bool isCoolingDown;
    [SerializeField] private bool hasCrate;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cart = FindObjectOfType<Crate>();
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        camPos.transform.position = new Vector3(0, 0, transform.position.z - 10);
        if (!isDashing) input = inputTemp;
        rb.velocity = speed * input * (isDashing ? 3 : 1) * (hasCrate ? .5f : 1);

        if(hasCrate)
        {
            if (rb.velocity == Vector3.zero) animator.Play("Idle_Carry");
            else animator.Play("Walk");
        }
        else
        {
            if (rb.velocity == Vector3.zero) animator.Play("Idle");
            else animator.Play("Run");
        }


        Vector3 direction = (transform.position + input) - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !cooldown)
        {
            input = Vector3.zero;
            StartCoroutine(Dash(1));
            input = Vector3.zero;
            Destroy(other.gameObject);
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
        StartCoroutine(PokeCooldown());
    }

    public void OnDash()
    {
       if(!cooldown && !hasCrate) StartCoroutine(Dash(.2f)); 
    }

    public void OnPickDrop()
    {
        if (Vector3.Distance(transform.position, cart.transform.position) < returnDistance)
        {
            if (hasCrate)
            {
                cart.Drop();
                cart.transform.parent = null;
            }
            else
            {
                cart.PickUp();
                cart.transform.rotation = Quaternion.identity;
                cart.transform.position = cratePos.position;
                cart.transform.parent = cratePos;
                cart.transform.rotation = Quaternion.identity;
            }
            hasCrate = !hasCrate;
        }
        camPos.SetActive(hasCrate);
    }

    IEnumerator Dash(float time)
    {
        isDashing = true;
        cooldown = true;
        yield return new WaitForSeconds(time);
        isDashing = false;
        yield return new WaitForSeconds(.7f);
        cooldown = false;
    }

    IEnumerator PokeCooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
    }
}