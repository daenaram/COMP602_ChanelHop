using UnityEngine;

[System.Serializable]  // Fixed typo in Serializable attribute
public class SaveData
{
    // Player Position
    public Vector3 playerPosition;  // Fixed case in Vector3

    // Player Health
    public float currentHealth;
    
    // Current Weapon
    public FloatingWeapon.WeaponType currentWeapon;
    
    // Current Scene
    public string currentSceneName;

    // Constructor
    public SaveData()
    {
        playerPosition = Vector3.zero;
        currentHealth = 0f;
        currentWeapon = FloatingWeapon.WeaponType.None;
        currentSceneName = "";
    }
}
