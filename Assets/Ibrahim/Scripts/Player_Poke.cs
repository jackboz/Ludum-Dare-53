using UnityEngine;

public class Player_Poke : MonoBehaviour
{
    [SerializeField] private Transform spherePos;
    [SerializeField] private float sphereRadius;

    public void OnPoke()
    {
        Debug.Log("OnPokeInput");
        Collider[] coll = Physics.OverlapSphere(spherePos.position, sphereRadius);
        if (coll.Length == 0) return;
        foreach (Collider collider in coll) collider.GetComponent<IPokeble>()?.OnPoke();
    }
}
