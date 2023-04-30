using TMPro;
using UnityEngine;

public class DumplingsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] Crate crate;

    private void FixedUpdate()
    {
        textMeshPro.text = crate.goodsAmount.ToString();
    }
}