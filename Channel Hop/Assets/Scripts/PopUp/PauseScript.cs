using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseButton;

    public void Awake()
    {
        pauseMenu.SetActive(false); // Ensure the pause menu is inactive at the start
        pauseButton.interactable = true; // Ensure the pause button is interactable
    }

    public void Update()
    {
    }
    public void pause()
    {
        pauseMenu.SetActive(true); // Activate the pause menu
        Time.timeScale = 0; // Pause the game
        pauseButton.interactable = false; // Disable the pause button
    }

    public void home()
    {
        Debug.Log("going home");
        SceneManager.LoadScene("Menu");
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Resume the game
        pauseButton.interactable = true; // Disable the pause button
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

}
