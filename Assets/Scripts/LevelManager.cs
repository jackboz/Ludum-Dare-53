using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    static public int TotalBuns = 0;

    private void Start()
    {
        Debug.Log("Total buns " + TotalBuns);
    }

    public void LoadNextLevel()
    {
        if (nextLevel != "")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
