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
    [SerializeField] private GameObject swordSlashPrefab;  // Add this
    [SerializeField] private GameObject axeSlashPrefab;    // Add this

    [Header("Attack Ranges")]
    [SerializeField] private float meleeRange = 1.5f;
    [SerializeField] private float staffAoeRadius = 2f;
    [SerializeField] private float swordRadius = 1f;
    [SerializeField] private float axeRadius = 1.5f;

    [Header("Attack Points")]
    [SerializeField] private Transform swordAttackPoint;
    [SerializeField] private Transform axeAttackPoint;
    [SerializeField] private Transform rangedAttackPoint;  // For both staff and bow
    [SerializeField] private LayerMask enemyLayer; // Layer containing enemies

    [Header("Components")]
    [SerializeField] private SpriteRenderer characterSprite; // Reference to player's sprite renderer

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
            // Spawn sword slash effect
            GameObject slashEffect = Instantiate(swordSlashPrefab, swordAttackPoint.position, swordAttackPoint.rotation);
            Destroy(slashEffect, 0.5f); // Destroy after animation

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(swordAttackPoint.position, swordRadius, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                IDamageable damageable = enemy.GetComponent<IDamageable>();
                damageable?.TakeDamage(1);
                Debug.Log($"Hit enemy: {enemy.name}");
            }
            nextAttackTime = Time.time + swordAttackSpeed;
        }
    }

    private void HandleAxeAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Spawn axe slash effect
            GameObject slashEffect = Instantiate(axeSlashPrefab, axeAttackPoint.position, axeAttackPoint.rotation);
            Destroy(slashEffect, 0.8f); // Longer duration for slower axe animation

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(axeAttackPoint.position, axeRadius, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                IDamageable damageable = enemy.GetComponent<IDamageable>();
                damageable?.TakeDamage(2);
                Debug.Log($"Hit enemy with axe: {enemy.name}");
            }
            nextAttackTime = Time.time + axeAttackSpeed;
        }
    }

    private void HandleStaffAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Get the facing direction based on character's sprite
            float facingDirection = characterSprite.flipX ? -1f : 1f;
            Vector3 shootDirection = Vector3.right * facingDirection;

            GameObject projectile = Instantiate(staffProjectilePrefab, rangedAttackPoint.position, rangedAttackPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = shootDirection * staffProjectileSpeed;

            // If projectile has a sprite renderer, match the facing direction
            SpriteRenderer projectileSprite = projectile.GetComponent<SpriteRenderer>();
            if (projectileSprite != null)
            {
                projectileSprite.flipX = characterSprite.flipX;
            }

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
                // Get the facing direction based on character's sprite
                float facingDirection = characterSprite.flipX ? -1f : 1f;
                Vector3 shootDirection = Vector3.right * facingDirection;

                GameObject arrow = Instantiate(arrowPrefab, rangedAttackPoint.position, rangedAttackPoint.rotation);
                Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
                rb.linearVelocity = shootDirection * bowArrowSpeed;

                // If arrow has a sprite renderer, match the facing direction
                SpriteRenderer arrowSprite = arrow.GetComponent<SpriteRenderer>();
                if (arrowSprite != null)
                {
                    arrowSprite.flipX = characterSprite.flipX;
                }

                Arrow arrowComp = arrow.AddComponent<Arrow>();
                arrowComp.Initialize(2);
            }
            isChargingBow = false;
            nextAttackTime = Time.time + 0.2f;
        }
    }

    // Add visual debugging for different attack ranges
    private void OnDrawGizmosSelected()
    {
        if (weaponController == null) return;

        switch (weaponController.GetCurrentWeaponType())
        {
            case FloatingWeapon.WeaponType.Sword:
                if (swordAttackPoint != null)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(swordAttackPoint.position, swordRadius);
                }
                break;
            case FloatingWeapon.WeaponType.Axe:
                if (axeAttackPoint != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(axeAttackPoint.position, axeRadius);
                }
                break;
            case FloatingWeapon.WeaponType.Staff:
                if (rangedAttackPoint != null)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(rangedAttackPoint.position, staffAoeRadius);
                }
                break;
        }
    }
}
