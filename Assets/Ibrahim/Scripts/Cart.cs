using UnityEngine;

public class Cart : MonoBehaviour, IPokeble
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float strength = 1000;
    public int goodsAmount;

    public void OnPoke(Vector3 impulse)
    {
        Debug.Log("OnPoke");
        body.AddForce(impulse.normalized * strength, ForceMode.Impulse);
    }
}
