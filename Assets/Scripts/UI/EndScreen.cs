using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gotDumplings;

    // Start is called before the first frame update
    void Start()
    {
        gotDumplings.SetText("You saved " + LevelManager.TotalBuns + " dumplings!");
        Time.timeScale = 1.0f;
    }

    public void RTM()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}

