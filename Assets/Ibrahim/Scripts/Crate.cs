using UnityEngine;

public class Crate : MonoBehaviour
{
    //oD Wreckz made it public so it could be tested in inspector
    public int goodsAmount = 3;

    Player_Poke playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<Player_Poke>();
    }

    public void PickUp()
    {
        Destroy(GetComponent<BoxCollider>());
    }

    public void Drop()
    {
        gameObject.AddComponent<BoxCollider>();
    }

    public void StealGoods()
    {
        if (goodsAmount > 0) goodsAmount -= 1;
    }

    public void ReturnGoods(int amount)
    {
        goodsAmount += amount;
    }


    private void Update()
    {
        if (goodsAmount <= 0) Debug.LogError("YouLost");
        Camera.main.transform.position = new Vector3(0, 15, transform.position.z - 10);
    }
}
