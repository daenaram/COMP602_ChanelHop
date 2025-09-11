using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Weapon Renderers")]
    [SerializeField] private SpriteRenderer prefabSword;
    [SerializeField] private SpriteRenderer prefabAxe;
    [SerializeField] private SpriteRenderer prefabStaff;
    [SerializeField] private SpriteRenderer prefabBow;

    private FloatingWeapon.WeaponType equippedWeaponType;
    private FloatingWeapon currentFloatingWeapon;
    private bool hasWeapon = false;

    void Start()
    {
        equippedWeaponType = FloatingWeapon.WeaponType.Sword;
        currentFloatingWeapon = null;
        hasWeapon = false;
        
        // Disable all weapon renderers initially
        DisableAllWeapons();
    }

    private void DisableAllWeapons()
    {
        if (prefabSword != null) prefabSword.enabled = false;
        if (prefabAxe != null) prefabAxe.enabled = false;
        if (prefabStaff != null) prefabStaff.enabled = false;
        if (prefabBow != null) prefabBow.enabled = false;
    }

    public void EquipWeapon(FloatingWeapon.WeaponType weaponType, FloatingWeapon floatingWeapon)
    {
        // If player already has this weapon, ignore pickup
        if (hasWeapon && currentFloatingWeapon == floatingWeapon)
        {
            return;
        }

        // If player has a different weapon, reactivate it
        if (hasWeapon && currentFloatingWeapon != null)
        {
            currentFloatingWeapon.ReactivateWeapon();
        }

        // Store the new floating weapon reference
        currentFloatingWeapon = floatingWeapon;
        equippedWeaponType = weaponType;

        // Disable all weapons first
        DisableAllWeapons();

        // Enable the appropriate weapon renderer
        switch (weaponType)
        {
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

    public bool HasWeapon()
    {
        return hasWeapon;
    }

    public FloatingWeapon.WeaponType GetCurrentWeaponType()
    {
        return equippedWeaponType;
    }
}
