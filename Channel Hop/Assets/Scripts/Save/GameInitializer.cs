using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    // Apply saved data to the player when the scene starts
    private void Start()
    {
        // Check if there is saved data to apply
        if (SaveController.SaveDataToApply != null)
        {
            Time.timeScale = 1; // Ensure game is not paused
            SaveData data = SaveController.SaveDataToApply;

            // Find player in the scene
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // Apply saved position, health, and weapon
                player.transform.position = data.playerPosition;
                player.GetComponent<Health>().SetHealth(data.currentHealth);
                player.GetComponent<PlayerWeaponController>().EquipWeapon(data.currentWeapon, null);
                player.GetComponent<PlayerRespawn>().SetCheckpointPosition(data.checkpointPosition);

                
            }

            // Clear static save data after applying
            SaveController.SaveDataToApply = null;
        }
    }
}
