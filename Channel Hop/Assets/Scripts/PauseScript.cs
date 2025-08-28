using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Start()
    {
        pauseMenu.SetActive(false); // Ensure the pause menu is inactive at the start
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
    }
    //public void pause()
    //{
    //    pauseMenu.SetActive(true); // Activate the pause menu
    //}

    //public void home()
    //{
        
    //}

    //public void resume()
    //{
    //    pauseMenu.SetActive(false);
    //}

    //public void restart()
    //{
        
    //}

}
