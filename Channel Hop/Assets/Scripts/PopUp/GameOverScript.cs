using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] public GameObject gameOver;
    [SerializeField] private GameObject exitPopUp;
    [SerializeField] private Health player1HP;
    [SerializeField] private Health player2HP;
    [SerializeField] private PlayerRespawn player1Respawn;
    [SerializeField] private PlayerRespawn player2Respawn;
    [SerializeField] SaveUI saveUI;
    [SerializeField] private SaveController saveController;
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
        GameObject player1 = GameObject.FindWithTag("Player1");
        GameObject player2 = GameObject.FindWithTag("Player2");

        if (player1 != null)
        {
            player1Movement = player1.GetComponent<PlayerMovement>();
            player1Attacking = player1.GetComponent<PlayerAttackingScript>();
            player1Anim = player1.GetComponent<Animator>();
            if (player1HP == null)
            {
                player1HP = player1.GetComponent<Health>();
            }
            // auto-assign player1Respawn if not set in inspector
            if (player1Respawn == null)
            {
                player1Respawn = player1.GetComponent<PlayerRespawn>();
            }
        }

        if (player2 != null)
        {
            player2Movement = player2.GetComponent<PlayerMovement>();
            player2Attacking = player2.GetComponent<PlayerAttackingScript>();
            player2Anim = player2.GetComponent<Animator>();
            if (player2HP == null)
            {
                player2HP = player2.GetComponent<Health>();
            }
            // auto-assign player2Respawn if not set in inspector
            if (player2Respawn == null)
            {
                player2Respawn = player2.GetComponent<PlayerRespawn>();
            }
        }

        // Initialize UI state
        if (gameOver != null) gameOver.SetActive(false);
        if (exitPopUp != null) exitPopUp.SetActive(false);
    }

    void Update()
    {
        if (player1HP == null)
        {
            Debug.LogError("Player1 HP reference not set in inspector!");
            return;
        }

        if (player2HP == null)
        {
            Debug.LogError("Player2 HP reference not set in inspector!");
            return;
        }

        if (player1HP.dead || player2HP.dead)
        {
            isGameOver = true;
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            DisablePlayerInput();
        }
    }

    public void revive()
    {
        Debug.Log("Revive called");
        Time.timeScale = 1f;
        isRevived = true;
        isGameOver = false;
        
        if (gameOver != null)
            gameOver.SetActive(false);

        // Reset health first
        if (player1HP != null)
        {
            player1HP.SetHealth(player1HP.startingHealth);
            player1HP.dead = false;
        }
        
        if (player2HP != null)
        {
            player2HP.SetHealth(player2HP.startingHealth);
            player2HP.dead = false;
        }

        // Then handle respawn
        if (player1Respawn != null)
            player1Respawn.Respawn();
        if (player2Respawn != null)
            player2Respawn.Respawn();

        EnablePlayerInput();

        // Reset animations
        if (player1Anim != null)
        {
            player1Anim.ResetTrigger("die");
            player1Anim.Play("Idle");
        }

        if (player2Anim != null)
        {
            player2Anim.ResetTrigger("die");
            player2Anim.Play("Idle");
        }
    }
    
    public void exit()
    {
        exitPopUp.SetActive(true);
    }

    public void yesExit()
    {
        saveController.SaveGame("LastSave.json");
        SceneManager.LoadScene("Menu");
    }
    public void noExit()
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