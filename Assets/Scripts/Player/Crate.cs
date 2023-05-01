using UnityEngine;

public class Crate : MonoBehaviour
{
    public int goodsAmount = 3;
    public float bunsGoneTimer;
    public GameObject[] dumplings;
    public GameObject deathScreen;
    public GameObject bunsAmountUI;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void PickUp()
    {
        soundManager.PlayLiftBoxSounds();
        Destroy(GetComponent<BoxCollider>());
    }

    public void Drop()
    {
        soundManager.PlayDropBoxSounds();
        gameObject.AddComponent<BoxCollider>();
    }

    public void StealGoods()
    {
        if (goodsAmount > 0)
        {
            goodsAmount -= 1;
            for (int i = 0; i < dumplings.Length; i++)
            {
                if (dumplings[i].activeInHierarchy)
                {
                    dumplings[i].gameObject.SetActive(false);
                    return;
                }
            }
        }

    }

    public void ReturnGoods(int amount)
    {
        goodsAmount += amount;
        for (int i = 0; i < dumplings.Length; i++)
        {
            if (!dumplings[i].activeInHierarchy)
            {
                dumplings[i].gameObject.SetActive(true);
                return;
            }
        }
    }


    private void FixedUpdate()
    {
        if (goodsAmount <= 0)
        {
            Debug.LogError("YouLost");
            bunsGoneTimer -= Time.deltaTime;

            if (bunsGoneTimer <= 0)
            {
                Time.timeScale = 0;
                deathScreen.SetActive(true);
                bunsAmountUI.SetActive(false);
            }
        }
        else
        {
            bunsGoneTimer = 5f;
        }
    }
}
