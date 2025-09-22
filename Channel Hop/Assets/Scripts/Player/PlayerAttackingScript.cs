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
    [SerializeField] private GameObject swordSlashPrefab;
    [SerializeField] private GameObject axeSlashPrefab;

    [Header("Attack Ranges")]
    [SerializeField] private float meleeRange = 1.5f;
    [SerializeField] private float staffAoeRadius = 2f;
    [SerializeField] private float swordRadius = 1f;
    [SerializeField] private float axeRadius = 1.5f;

    [Header("Attack Points")]
    [SerializeField] private Transform swordAttackPoint;
    [SerializeField] private Transform axeAttackPoint;
    [SerializeField] private Transform rangedAttackPoint;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Components")]
    [SerializeField] private SpriteRenderer characterSprite;

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
            // Slash effect
            GameObject slashEffect = Instantiate(swordSlashPrefab, swordAttackPoint.position, swordAttackPoint.rotation);
            Destroy(slashEffect, 0.5f);

            // Damage enemies
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(swordAttackPoint.position, swordRadius, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                Health health = enemy.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(1);
                    Debug.Log($"Sword hit: {enemy.name}");
                }
            }

            nextAttackTime = Time.time + swordAttackSpeed;
        }
    }

    private void HandleAxeAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Slash effect
            GameObject slashEffect = Instantiate(axeSlashPrefab, axeAttackPoint.position, axeAttackPoint.rotation);
            Destroy(slashEffect, 0.8f);

            // Damage enemies
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(axeAttackPoint.position, axeRadius, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                Health health = enemy.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(2);
                    Debug.Log($"Axe hit: {enemy.name}");
                }
            }

            nextAttackTime = Time.time + axeAttackSpeed;
        }
    }

    private void HandleStaffAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //facing direction
            float facingDirection = transform.localScale.x > 0 ? 1f : -1f;
            Vector3 shootDirection = new Vector3(facingDirection, 0f, 0f).normalized;

          
            GameObject projectile = Instantiate(staffProjectilePrefab, rangedAttackPoint.position, Quaternion.identity);

            //Apply velocity
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = shootDirection * staffProjectileSpeed;

           
            SpriteRenderer projectileSprite = projectile.GetComponent<SpriteRenderer>();
            if (projectileSprite != null)
                projectileSprite.flipX = (facingDirection < 0);

            
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
                float facingDirection = transform.localScale.x > 0 ? 1f : -1f;
                Vector3 shootDirection = Vector3.right * facingDirection;

                // Spawn arrow
                GameObject arrow = Instantiate(arrowPrefab, rangedAttackPoint.position, rangedAttackPoint.rotation);
                Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
                rb.linearVelocity = shootDirection * bowArrowSpeed;

                // Flip sprite if needed
                SpriteRenderer arrowSprite = arrow.GetComponent<SpriteRenderer>();
                if (arrowSprite != null)
                    arrowSprite.flipX = (facingDirection < 0);

                // Add Arrow behaviour
                Arrow arrowComp = arrow.AddComponent<Arrow>();
                arrowComp.Initialize(2);
            }

            isChargingBow = false;
            nextAttackTime = Time.time + 0.2f;
        }
    }

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

