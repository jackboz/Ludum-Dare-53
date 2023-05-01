using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public Pausemenuplaceholder playerController;
    public GameObject pauseMenuUI;

    //Get the player script
    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Pausemenuplaceholder>();
    }

    // Update is called once per frame
    void Update()
    {
        //So that the player cant pause after death
        if (/*player is not dead*/playerController)
        {
            //If start button or escape pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        //Take away UI and set time back to normal
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        //Add UI and set time to stopped
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        //When you return to menu reset paused time
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Resume();
    }

    public void Quit()
    {
        //Quit
        Application.Quit();
    }
}
