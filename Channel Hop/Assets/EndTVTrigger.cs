using UnityEngine;

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

                if (numCoins.totalCoins == 10)
                {
                    star1.SetActive(true);
                    star2.SetActive(true);
                    star3.SetActive(true);
                    Debug.Log("Player collected all coins!");
                }
                else if (numCoins.totalCoins >= 7)
                {
                    star1.SetActive(true);
                    star2.SetActive(true);
                    Debug.Log("Player collected at least 7 coins!");
                }
                else if (numCoins.totalCoins >= 4)
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


}
