using System.Collections;
using UnityEngine;

public class Player_Poke : MonoBehaviour
{
    public float returnDistance = 3f;
    [SerializeField] private Transform spherePos;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float cooldownTime;
    [SerializeField] private bool isCoolingDown;

    Inventory playerInventory;
    Cart cart;

    private void Awake()
    {
        cart = FindObjectOfType<Cart>();
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, cart.transform.position) < returnDistance)
        { 
            playerInventory.ReturnGoods();
        }
    }

    public void OnPoke()
    {
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
