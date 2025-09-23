using UnityEngine;


/// Controls the weapon system for the player, managing weapon pickups and displays

public class PlayerWeaponController : MonoBehaviour
{
    // References to the visual representations of each weapon type
    [Header("Weapon Renderers")]
    [SerializeField] private SpriteRenderer prefabSword;
    [SerializeField] private SpriteRenderer prefabAxe;
    [SerializeField] private SpriteRenderer prefabStaff;
    [SerializeField] private SpriteRenderer prefabBow;

    // Tracks the currently equipped weapon type and its corresponding floating weapon
    private FloatingWeapon.WeaponType equippedWeaponType;
    private FloatingWeapon currentFloatingWeapon;
    private bool hasWeapon = false;

    // Initialize weapon state when the script starts
    void Start()
    {
        equippedWeaponType = FloatingWeapon.WeaponType.Sword;
        currentFloatingWeapon = null;
        hasWeapon = false;
        
        DisableAllWeapons();
    }

    // Helper method to ensure all weapon sprites are hidden
    private void DisableAllWeapons()
    {
        if (prefabSword != null) prefabSword.enabled = false;
        if (prefabAxe != null) prefabAxe.enabled = false;
        if (prefabStaff != null) prefabStaff.enabled = false;
        if (prefabBow != null) prefabBow.enabled = false;
    }

    // Called when player attempts to pick up a weapon
    public void EquipWeapon(FloatingWeapon.WeaponType weaponType, FloatingWeapon floatingWeapon)
    {
        // Prevent picking up the same weapon twice
        if (hasWeapon && currentFloatingWeapon == floatingWeapon)
        {
            return;
        }

        // Drop current weapon if picking up a different one
        if (hasWeapon && currentFloatingWeapon != null)
        {
            currentFloatingWeapon.ReactivateWeapon();
        }

        // Update weapon tracking variables
        currentFloatingWeapon = floatingWeapon;
        equippedWeaponType = weaponType;

        // Hide all weapon sprites before showing the new one
        DisableAllWeapons();

        // Show the appropriate weapon sprite based on type
        switch (weaponType)
        {   
            case FloatingWeapon.WeaponType.None:
                break;
            case FloatingWeapon.WeaponType.Sword:
                if (prefabSword != null) prefabSword.enabled = true;
                break;
            case FloatingWeapon.WeaponType.Axe:
                if (prefabAxe != null) prefabAxe.enabled = true;
                break;
            case FloatingWeapon.WeaponType.Staff:
                if (prefabStaff != null) prefabStaff.enabled = true;
                break;
            case FloatingWeapon.WeaponType.Bow:
                if (prefabBow != null) prefabBow.enabled = true;
                break;
            
        }
        
        hasWeapon = true;
    }

    // Returns whether the player currently has a weapon equipped
    public bool HasWeapon()
    {
        return hasWeapon;
    }

    // Returns the type of weapon currently equipped
    public FloatingWeapon.WeaponType GetCurrentWeaponType()
    {
        return equippedWeaponType;
    }
}
