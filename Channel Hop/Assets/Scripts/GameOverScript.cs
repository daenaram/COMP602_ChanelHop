using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    private Health playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameOver.SetActive(false); // Ensure the pause menu is inactive at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.isDead())
        {
            gameOver.SetActive(true); // Activate the game over menu
            Debug.Log("game over");
        }
    }

    public void revive()
    {
        Debug.Log("reviving");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void exit()
    {
        Debug.Log("exiting");
        SceneManager.LoadScene("Menu");
    }

}