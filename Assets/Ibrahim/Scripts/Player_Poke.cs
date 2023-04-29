using System.Collections;
using UnityEngine;

public class Player_Poke : MonoBehaviour
{
    public float returnDistance = 3f;
    [SerializeField] private Transform spherePos;
    [SerializeField] private Transform cratePos;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float cooldownTime;
    [SerializeField] private bool isCoolingDown;
    public bool hasCrate;

    Crate cart;

    private void Awake()
    {
        cart = FindObjectOfType<Crate>();
    }

    private void Update()
    {
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

    public void OnPoke()
    {
        if (hasCrate) return;
        Debug.Log("OnPokeInput");
        if (isCoolingDown) return;
        int layerMask = 1 << 6;
        Collider[] coll = Physics.OverlapSphere(spherePos.position, sphereRadius, layerMask);
        if (coll.Length == 0) return;
        Debug.Log(transform.forward);
        foreach (Collider collider in coll) collider.GetComponent<IPokeble>()?.OnPoke(transform.forward);
        StartCoroutine(cooldown());
    }

    IEnumerator cooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
    }
}
