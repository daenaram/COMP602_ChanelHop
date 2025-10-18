using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public int value;
    private bool hasTriggered;
    private CoinManager coinManager;

    private void Start()
    {
        coinManager = CoinManager.instance;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Player1" || collision.tag == "Player2") && !hasTriggered)
        {
            hasTriggered = true;
            coinManager.ChangeCoin(value);
            Destroy(gameObject);
        }
    }
}
