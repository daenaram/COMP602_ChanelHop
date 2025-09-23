using UnityEngine;

[System.Serializable]  // Allow saving/loading with JsonUtility
public class SaveData
{   //player1
    public Vector3 playerPosition1;                // Player's position in the scene
    public float currentHealth1;                   // Player's current health
    public FloatingWeapon.WeaponType currentWeapon1; // Currently equipped weapon
    public string currentSceneName;               // Name of the scene the player is in
    public Vector3 checkpointPosition1;
    //player 2
    public Vector3 playerPosition2;                // Player's position in the scene
    public float currentHealth2;                   // Player's current health
    public FloatingWeapon.WeaponType currentWeapon2; // Currently equipped weapon
    public Vector3 checkpointPosition2;            // Player's current checkpoint
    // Default constructor
    public SaveData()
    {
        currentSceneName = "";                    // Empty scene name by default
        playerPosition1 = Vector3.zero;           // Default position at origin
        currentHealth1 = 0f;                      // Default health
        currentWeapon1 = FloatingWeapon.WeaponType.None; // No weapon by default
        checkpointPosition1 = Vector3.zero;       // No checkpoint by default
        playerPosition2 = Vector3.zero;           // Default position at origin
        currentHealth2 = 0f;                      // Default health
        currentWeapon2 = FloatingWeapon.WeaponType.None; // No weapon by default
        checkpointPosition2 = Vector3.zero;       // No checkpoint by default
    }
}
