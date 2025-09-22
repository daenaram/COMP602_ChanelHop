using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Pit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the pit
        if (other.CompareTag("Player"))
        {
            // Call the player's death or respawn method
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerHealth.currentHealth); // instantly kills the player
            }
        }
    }
}