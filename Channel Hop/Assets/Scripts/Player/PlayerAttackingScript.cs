using UnityEngine;

public class PlayerAttackingScript : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float swordAttackSpeed = 0.3f;
    [SerializeField] private float axeAttackSpeed = 1f;
    [SerializeField] private float staffProjectileSpeed = 8f;
    [SerializeField] private float bowChargeTime = 0.5f;
    [SerializeField] private float bowArrowSpeed = 15f;

    [Header("Projectile Prefabs")]
    [SerializeField] private GameObject staffProjectilePrefab;
    [SerializeField] private GameObject arrowPrefab;

    [Header("Attack Ranges")]
    [SerializeField] private float meleeRange = 1.5f;
    [SerializeField] private float staffAoeRadius = 2f;

    private PlayerWeaponController weaponController;
    private float nextAttackTime = 0f;
    private float bowChargeStartTime = 0f;
    private bool isChargingBow = false;

    private void Start()
    {
        weaponController = GetComponent<PlayerWeaponController>();
    }

    private void Update()
    {
        if (!weaponController.HasWeapon()) return;
        HandleAttackInput();
    }

    private void HandleAttackInput()
    {
        if (Time.time < nextAttackTime) return;

        switch (weaponController.GetCurrentWeaponType())
        {
            case FloatingWeapon.WeaponType.Sword:
                HandleSwordAttack();
                break;
            case FloatingWeapon.WeaponType.Axe:
                HandleAxeAttack();
                break;
            case FloatingWeapon.WeaponType.Staff:
                HandleStaffAttack();
                break;
            case FloatingWeapon.WeaponType.Bow:
                HandleBowAttack();
                break;
        }
    }

    private void HandleSwordAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, meleeRange);
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                IDamageable enemy = hit.collider.GetComponent<IDamageable>();
                enemy?.TakeDamage(1);
            }
            nextAttackTime = Time.time + swordAttackSpeed;
        }
    }

    private void HandleAxeAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, meleeRange);
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                IDamageable enemy = hit.collider.GetComponent<IDamageable>();
                enemy?.TakeDamage(2);
            }
            nextAttackTime = Time.time + axeAttackSpeed;
        }
    }

    private void HandleStaffAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject projectile = Instantiate(staffProjectilePrefab, transform.position, transform.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = transform.right * staffProjectileSpeed;

            StaffProjectile staffProj = projectile.AddComponent<StaffProjectile>();
            staffProj.Initialize(staffAoeRadius, 1);
            nextAttackTime = Time.time + 0.5f;
        }
    }

    private void HandleBowAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isChargingBow = true;
            bowChargeStartTime = Time.time;
        }
        else if (Input.GetKeyUp(KeyCode.F) && isChargingBow)
        {
            float chargeTime = Time.time - bowChargeStartTime;
            if (chargeTime >= bowChargeTime)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
                Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
                rb.linearVelocity = transform.right * bowArrowSpeed;

                Arrow arrowComp = arrow.AddComponent<Arrow>();
                arrowComp.Initialize(2);
            }
            isChargingBow = false;
            nextAttackTime = Time.time + 0.2f;
        }
    }
}
