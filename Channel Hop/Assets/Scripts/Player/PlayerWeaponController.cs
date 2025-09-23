using UnityEngine;

/// Manages weapon pickups and displays for a specific player
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
        equippedWeaponType = FloatingWeapon.WeaponType.None;
        currentFloatingWeapon = null;
        hasWeapon = false;
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
        if (hasWeapon && currentFloatingWeapon == floatingWeapon) return;

        // Drop current weapon back into world
        if (hasWeapon && currentFloatingWeapon != null)
        {
            currentFloatingWeapon.ReactivateWeapon();
        }

        currentFloatingWeapon = floatingWeapon;
        equippedWeaponType = weaponType;

        DisableAllWeapons();
        switch (weaponType)
        {
            case FloatingWeapon.WeaponType.Sword: if (prefabSword != null) prefabSword.enabled = true; break;
            case FloatingWeapon.WeaponType.Axe:   if (prefabAxe != null) prefabAxe.enabled = true; break;
            case FloatingWeapon.WeaponType.Staff: if (prefabStaff != null) prefabStaff.enabled = true; break;
            case FloatingWeapon.WeaponType.Bow:   if (prefabBow != null) prefabBow.enabled = true; break;
        }

        hasWeapon = true;
    }

    public bool HasWeapon() => hasWeapon;
    public FloatingWeapon.WeaponType GetCurrentWeaponType() => equippedWeaponType;
}
