using System.Collections;
using UnityEngine;

public class Player_Poke : MonoBehaviour
{
    [SerializeField] private Transform spherePos;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float cooldownTime;
    [SerializeField] private bool isCoolingDown;

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
