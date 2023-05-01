using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI gotDumplings;

    private void Awake()
    {
        gotDumplings = transform.Find("DumplingWinLabel").GetComponent<TextMeshProUGUI>();
    }

    public void ShowWinEndText()
    {
        winPanel.SetActive(true);
        gotDumplings.SetText("You saved " + LevelManager.TotalBuns + " dumplings");
    }
}
