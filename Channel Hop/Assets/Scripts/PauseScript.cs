using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Awake()
    {
        pauseMenu.SetActive(false); // Ensure the pause menu is inactive at the start
    }

    public void Update()
    {
    }
    public void pause()
    {
        pauseMenu.SetActive(true); // Activate the pause menu
        Time.timeScale = 0; // Pause the game
    }

    public void home()
    {

    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    public void restart()
    {

    }

}
