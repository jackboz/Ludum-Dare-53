using TMPro;
using UnityEngine;

public class DumplingsUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public GameObject crateObject; // Assign the game object with the "Crate" script in the Inspector

    private void Update()
    {
        Crate crateScript = crateObject.GetComponent<Crate>();
        int dumplingAmount = crateScript.goodsAmount;

        textMeshPro.text = "Dumpling amount = " + dumplingAmount.ToString();
    }
}