using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Crate cart;
    Animator animator;
    Enemy_Spawner spawner;

    int triggerCount;
    private Vector3 inputTemp;
    private Vector3 input;
    private bool isDashing;
    private int speedMultiplie = 1;
    private bool cooldown;
    [SerializeField] private float returnDistance = 3f;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float speed;
    [SerializeField] private Transform spherePos;
    [SerializeField] private Transform cratePos;
    [SerializeField] private Transform cratePos2;
    [SerializeField] private GameObject camPos;
    [SerializeField] private bool isCoolingDown;
    [SerializeField] private bool hasCrate;
    private SoundManager soundManager;
    [SerializeField] private int parcelToSkip = 1;


    private void Start()
    {
        spawner = FindObjectOfType<Enemy_Spawner>();
        animator = GetComponent<Animator>();
        cart = FindObjectOfType<Crate>();
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void FixedUpdate()
    {
        camPos.transform.position = new Vector3(0, 0, transform.position.z - 10);
        if (!isDashing) input = inputTemp;
        rb.velocity = (hasCrate ? .5f : 1) * (isDashing ? 3 : 1) * speed * input * speedMultiplie;

        if (input == Vector3.zero && isDashing)
        {
            animator.Play("Drunk");
        }
        else
        {
            if (speedMultiplie == 0) animator.Play("Attack");
            else
            {
                if (hasCrate)
                {
                    if (rb.velocity == Vector3.zero) animator.Play("Idle_Carry");
                    else animator.Play("Walk");
                }
                else
                {
                    if (rb.velocity == Vector3.zero) animator.Play("Idle");
                    else animator.Play("Run");
                }
            }
        }


        Vector3 direction = (transform.position + input) - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }

    private void Update()
    {
        camPos.transform.position = new Vector3(0, 0, transform.position.z - 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !cooldown)
        {
            if (hasCrate) OnPickDrop();
            input = Vector3.zero;
            StartCoroutine(Dash(1));
            input = Vector3.zero;
            soundManager.PlayHitByShuriken();
            Destroy(other.gameObject);
        }

        if(other.CompareTag("Trigger"))
        {
            Destroy(other.gameObject);
            triggerCount++;
            spawner.InitiateEnemyWave();
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
        soundManager.PlayGirlAttackSounds();
        Collider[] coll = Physics.OverlapSphere(spherePos.position, sphereRadius, layerMask);
        if (coll.Length != 0) foreach (Collider collider in coll) collider.GetComponent<IPokeble>()?.OnPoke(transform.forward);
        StartCoroutine(PokeCooldown());
    }

    public void OnDash()
    {
        if (!cooldown && !hasCrate) StartCoroutine(Dash(.2f));
    }

    public void OnPickDrop()
    {
        if (Vector3.Distance(transform.position, cart.transform.position) < returnDistance)
        {
            if (hasCrate)
            {
                cart.Drop();
                cart.transform.parent = null;
                cart.transform.position = cratePos2.position;
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
        speedMultiplie = 0;
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownTime / 2);
        speedMultiplie = 1;
        yield return new WaitForSeconds(cooldownTime / 2);
        isCoolingDown = false;
    }
}