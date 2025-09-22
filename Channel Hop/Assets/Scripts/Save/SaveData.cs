using UnityEngine;

[System.Serializable]  // Allow saving/loading with JsonUtility
public class SaveData
{
    public Vector3 playerPosition;                // Player's position in the scene
    public float currentHealth;                   // Player's current health
    public FloatingWeapon.WeaponType currentWeapon; // Currently equipped weapon
    public string currentSceneName;               // Name of the scene the player is in

    // Default constructor
    public SaveData()
    {
        playerPosition = Vector3.zero;           // Default position at origin
        currentHealth = 0f;                      // Default health
        currentWeapon = FloatingWeapon.WeaponType.None; // No weapon by default
        currentSceneName = "";                    // Empty scene name by default
    }
}
