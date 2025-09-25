using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Health player1HP;
    [SerializeField] private Health player2HP;
    [SerializeField] private PlayerRespawn player1Respawn;
    [SerializeField] private PlayerRespawn player2Respawn;
    public bool isGameOver = false;
    private PlayerMovement player1Movement;
    private PlayerMovement player2Movement;
    private PlayerAttackingScript player1Attacking;
    private PlayerAttackingScript player2Attacking;
    private bool isRevived = false;
    private Animator player1Anim;
    private Animator player2Anim;

    void Start()
    {
        player1Movement = GameObject.FindWithTag("Player1").GetComponent<PlayerMovement>();
        player1Attacking = GameObject.FindWithTag("Player1").GetComponent<PlayerAttackingScript>();
        player1Anim = GameObject.FindWithTag("Player1").GetComponent<Animator>();

        player2Movement = GameObject.FindWithTag("Player2").GetComponent<PlayerMovement>();
        player2Attacking = GameObject.FindWithTag("Player2").GetComponent<PlayerAttackingScript>();
        player2Anim = GameObject.FindWithTag("Player2").GetComponent<Animator>();

        player1Respawn.SetGameOver(this);
        player2Respawn.SetGameOver(this);
        gameOver.SetActive(false);
    }

    void Update()
    {
        if (GameObject.FindWithTag("Player1").GetComponent<Health>().dead || GameObject.FindWithTag("Player2").GetComponent<Health>().dead)
        {
            isGameOver = true;
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            // Debug.Log("GameOver from GameOverScript");
        }
        //else if (player1HP.dead || player2HP.dead)
        //{
        //    DisablePlayerInput();
        //}
        //else
        //{
        //    EnablePlayerInput();
        //}
    }

    public void revive()
    {
        Time.timeScale = 1f;
        isRevived = true;
        isGameOver = false;
        gameOver.SetActive(false);

        player1Respawn.Respawn();
        player2Respawn.Respawn();

        player1HP.SetHealth(player1HP.startingHealth);
        player2HP.SetHealth(player2HP.startingHealth);

        //AddHealth(startingHealth);
        player1Anim.ResetTrigger("die");
        player1Anim.Play("Idle");

        player2Anim.ResetTrigger("die");
        player2Anim.Play("Idle"); ;



        Debug.Log("Revive called from GameOverScript");

    }

    public void exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void activateGameOver()
    {
        gameOver.SetActive(true);
    }
    private void DisablePlayerInput()
    {
        if (player1Movement != null) player1Movement.enabled = false;
        if (player1Attacking != null) player1Attacking.enabled = false;
        if (player2Movement != null) player2Movement.enabled = false;
        if (player2Attacking != null) player2Attacking.enabled = false;
    }

    private void EnablePlayerInput()
    {
        if (player1Movement != null) player1Movement.enabled = true;
        if (player1Attacking != null) player1Attacking.enabled = true;
        if (player2Movement != null) player2Movement.enabled = true;
        if (player2Attacking != null) player2Attacking.enabled = true;
    }

    public bool getRevived()
    {
        return isRevived;
    }
}