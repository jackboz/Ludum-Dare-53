using UnityEngine;

public class Cart : MonoBehaviour, IPokeble
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float strength = 1000;
    int goodsAmount;

    public void OnPoke(Vector3 impulse)
    {
        Debug.Log("OnPoke");
        body.AddForce(impulse.normalized * strength, ForceMode.Impulse);
    }

    public void StealGoods()
    {
        if (goodsAmount > 0)
        {
            goodsAmount -= 1;
        }
    }

    public void ReturnGoods(int amount)
    {
        goodsAmount += amount;
    }

    
    private void Update()
    {
        Camera.main.transform.position = new Vector3(0, 15, transform.position.z -10);
    }
}
