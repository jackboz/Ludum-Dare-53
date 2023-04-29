using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int goodsAmount = 0;
    Cart cart;

    private void Awake()
    {
        cart = FindObjectOfType<Cart>();
    }

    public void AddGoods()
    {
        goodsAmount += 1;
        Debug.Log("Player added goods. Now: " + goodsAmount);
    }

    public void ReturnGoods()
    {
        if (goodsAmount == 0) return;
        cart.ReturnGoods(goodsAmount);
        Debug.Log("Player returned " + goodsAmount + " goods.");
        goodsAmount = 0;
    }
}
