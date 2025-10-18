using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTVTrigger : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    [SerializeField] CoinManager numCoins;
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    private PlayerMovement player1Movement;
    private PlayerMovement player2Movement;
    private PlayerAttackingScript player1Attacking;
    private PlayerAttackingScript player2Attacking;
    [SerializeField]private int threeStar = 10;
    [SerializeField]private int twoStar = 7;
    [SerializeField]private int oneStar = 4;
    private void Start()
    {
        endPanel.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        Debug.Log(numCoins);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Debug.Log($"{collision.name} reached the TV!");
            if (endPanel != null)
            {
                endPanel.SetActive(true);
                Debug.Log("End panel activated!");
                Time.timeScale = 0f;
                DisablePlayerInput();

                if (numCoins.totalCoins >= threeStar)
                {
                    star1.SetActive(true);
                    star2.SetActive(true);
                    star3.SetActive(true);
                    Debug.Log("Player collected all coins!");
                }
                else if (numCoins.totalCoins >= twoStar)
                {
                    star1.SetActive(true);
                    star2.SetActive(true);
                    Debug.Log("Player collected at least 7 coins!");
                }
                else if (numCoins.totalCoins >= oneStar)
                {
                    star1.SetActive(true);
                    Debug.Log("Player collected at least 4 coins!");
                }

                
            }
            else
            {
                Debug.LogWarning("End panel reference is missing!");
            }
        }
    }

    private void DisablePlayerInput()
    {
        if (player1Movement != null) player1Movement.enabled = false;
        if (player1Attacking != null) player1Attacking.enabled = false;
        if (player2Movement != null) player2Movement.enabled = false;
        if (player2Attacking != null) player2Attacking.enabled = false;
    }

    public void nextLvl()
    {
        SceneManager.LoadScene("Level2");
    }
}
