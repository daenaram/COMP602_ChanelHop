using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private int totalCoins;

    [SerializeField] private TMP_Text coinText;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

    }
    private void OnGUI()
    {
        coinText.text = totalCoins.ToString();
    }
    public void ChangeCoin(int amount)
    {
        totalCoins += amount;
        Debug.Log("Total Coins: " + totalCoins);
    }
}


