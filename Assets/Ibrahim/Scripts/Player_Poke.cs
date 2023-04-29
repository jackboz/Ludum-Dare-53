using UnityEngine;

public class Player_Poke : MonoBehaviour
{
    [SerializeField] private Transform spherePos;
    [SerializeField] private float sphereRadius;

    public void OnPoke()
    {
        Debug.Log("OnPokeInput");
        int layerMask = 1 << 6;
        Collider[] coll = Physics.OverlapSphere(spherePos.position, sphereRadius, layerMask);
        if (coll.Length == 0) return;
        foreach (Collider collider in coll) collider.GetComponent<IPokeble>()?.OnPoke(Vector3.forward);
    }
}
