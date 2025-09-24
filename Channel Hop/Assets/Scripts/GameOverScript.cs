using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] public Health player1HP;
    [SerializeField] public Health player2HP;
    [SerializeField] private PlayerRespawn player1Respawn;
    [SerializeField] private PlayerRespawn player2Respawn;
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
        if (!isGameOver &&
        player1HP != null && player2HP != null &&
        player1HP.dead && player2HP.dead)
        {
            isGameOver = true;
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            Debug.Log("Game Over triggered");
        }

    }

    public void revive()
    {
        Debug.Log("Reviving both players...");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (player1Respawn != null)
        {
            Debug.Log("Calling Player1Respawn.Respawn()");
            player1Respawn.Respawn();
        }

        if (player2Respawn != null)
        {
            Debug.Log("Calling Player2Respawn.Respawn()");
            player2Respawn.Respawn();
        }
        // playerRespawn.Respawn();
        Time.timeScale = 1;
        gameOver.SetActive(false);
        isGameOver = false;
        Debug.Log("Player respawned from GameOverScript.cs");
        
    }

    public void exit()
    {
        Debug.Log("exiting");
        SceneManager.LoadScene("Menu");
    }

    

}