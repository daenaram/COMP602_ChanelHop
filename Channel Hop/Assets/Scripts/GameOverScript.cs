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
    private Health hp;
    public bool isGameOver = false;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameOver.SetActive(false);
        anim = GetComponent<Animator>();
       //float healthStart = hp.startingHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        // show game over panel if player is dead
        if (player1HP.dead && player2HP.dead)
        {
            isGameOver = true;
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            Debug.Log("Game Over triggered");
        }
        
    }

    public void revive()
    {

        isGameOver = false; // lock immediately
        Debug.Log("Revive() CALLED");

        if (player1Respawn != null)
            player1Respawn.Respawn();

        if (player2Respawn != null)
            player2Respawn.Respawn();

        hp.SetHealth(hp.startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        Time.timeScale = 1;
        gameOver.SetActive(false);

    }

    public void exit()
    {
        Debug.Log("exiting");
        SceneManager.LoadScene("Menu");
    }

    public void activateGameOver()
    {
        gameOver.SetActive(true);
    }


}