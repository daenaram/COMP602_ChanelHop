using UnityEngine;

public class StaminaIndicator : MonoBehaviour
{
    private void Start()
    {
        // Make sure the indicator follows the player
        transform.SetParent(transform.parent);
        // Position it above the player
        transform.localPosition = new Vector3(0, 0.9f, 0);
    }
}
