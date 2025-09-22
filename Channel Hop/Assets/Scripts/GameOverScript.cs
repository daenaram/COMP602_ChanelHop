using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    public Health playerHP;
    private bool isGameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // show game over panel if player is dead
        if (!isGameOver && playerHP != null && playerHP.dead) 
        {
            isGameOver = true;
            gameOver.SetActive(true);
            Time.timeScale = 0; // freeze the game
            Debug.Log("Game Over triggered");
        }

    }

    public void revive()
    {
        Debug.Log("reviving");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void exit()
    {
        Debug.Log("exiting");
        SceneManager.LoadScene("Menu");
    }

    

}