using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gotDumplings;

    // Start is called before the first frame update
    void Start()
    {
        gotDumplings.SetText("You saved " + LevelManager.TotalBuns + " dumplings!");
    }

}

