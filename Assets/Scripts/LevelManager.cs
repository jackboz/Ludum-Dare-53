using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevel;

    public void LoadNextLevel()
    {
        if (nextLevel != "")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
