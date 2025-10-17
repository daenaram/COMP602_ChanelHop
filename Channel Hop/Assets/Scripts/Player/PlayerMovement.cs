using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum Player { Player1, Player2 }
    public Player playerID = Player.Player1;

    [SerializeField] public float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private Transform player1;
    [SerializeField] private float maxDistance = 8f;

    // Double Jump variables
    private bool hasDoubleJump = true;
    
    // Dash variables
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    private bool isDashing = false;
    private bool canDash = true;
    private float dashTime;
    
    // Stamina variables
    private bool hasStamina = true;
    private float staminaRecoveryTimer = 0f;
    [SerializeField] private float staminaRecoveryTime = 2f;

    // Cloud VFX
    [SerializeField] private GameObject cloudVFXPrefab; // Assign your cloud particle effect in Inspector

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = 0f;
        bool jumpInput = false;
        bool dashInput = false;

        // Assign controls based on player
        if (playerID == Player.Player1)
        {
            horizontalInput = (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0);
            jumpInput = Input.GetKeyDown(KeyCode.W);
            dashInput = Input.GetKeyDown(KeyCode.LeftShift);
        }
        else if (playerID == Player.Player2)
        {
            horizontalInput = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
            jumpInput = Input.GetKeyDown(KeyCode.UpArrow);
            dashInput = Input.GetKeyDown(KeyCode.RightShift);
        }

        // Handle stamina recovery
        HandleStaminaRecovery();

        // Don't process other inputs during dash
        if (isDashing)
        {
            HandleDash();
            return;
        }

        // Flip sprite
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // Wall jump logic
        if (wallJumpCooldown < 0.2f)
        {
            // Handle jumping (single and double jump)
            if (jumpInput)
                HandleJump();

            // Handle movement
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 1;
                if (Mathf.Abs(horizontalInput) > 0)
                {
                    body.linearVelocity = new Vector2(0, body.linearVelocity.y);
                }
                else
                {
                    body.gravityScale = 0;
                    body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Lerp(body.linearVelocity.y, -1f, Time.deltaTime*10));
                }
            }
            else
            {
                body.gravityScale = 1;
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        // Handle dash input
        if (dashInput && canDash && hasStamina)
        {
            StartDash();
        }

        // clamp player2 - prevents moving too far from player1
        if (playerID == Player.Player2 && player1 != null)
        {
            float distance = transform.position.x - player1.position.x;

            if (distance > maxDistance)
                transform.position = new Vector3(player1.position.x + maxDistance, transform.position.y, transform.position.z);
            else if (distance < -maxDistance)
                transform.position = new Vector3(player1.position.x - maxDistance, transform.position.y, transform.position.z);
        }
    }

    private void HandleJump()
    {
        if (isGrounded())
        {
            // Normal jump
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpHeight);
            anim.SetTrigger("jump");
            hasDoubleJump = true; // Reset double jump when grounded
        }
        else if (hasDoubleJump && hasStamina)
        {
            // Double jump
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpHeight);
            anim.SetTrigger("jump");
            
            // Spawn cloud VFX for double jump
            SpawnCloudVFX();
            
            hasDoubleJump = false;
            hasStamina = false;
            staminaRecoveryTimer = 0f;
        }
    }

    private void SpawnCloudVFX()
    {
        if (cloudVFXPrefab != null)
        {
            // Calculate position slightly below the player's feet
            Vector3 cloudPosition = transform.position + new Vector3(0, -0.5f, 0);
            
            // Create rotation with specific z angle (536.963 degrees)
            Quaternion cloudRotation = Quaternion.Euler(0f, 0f, 536.963f);
            
            // Instantiate the cloud VFX with specific rotation
            GameObject cloudVFX = Instantiate(cloudVFXPrefab, cloudPosition, cloudRotation);
            
            // Start coroutine to destroy the cloud after 0.5 seconds
            StartCoroutine(DestroyCloudAfterDelay(cloudVFX, 0.1f));
        }
        else
        {
            Debug.LogWarning("Cloud VFX Prefab is not assigned in the Inspector!");
        }
    }

    private IEnumerator DestroyCloudAfterDelay(GameObject cloudObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (cloudObject != null)
        {
            Destroy(cloudObject);
        }
    }

    private void HandleStaminaRecovery()
    {
        if (!hasStamina)
        {
            staminaRecoveryTimer += Time.deltaTime;
            if (staminaRecoveryTimer >= staminaRecoveryTime)
            {
                hasStamina = true;
                staminaRecoveryTimer = 0f;
                canDash = true;
            }
        }
    }

    private void StartDash()
    {
        isDashing = true;
        canDash = false;
        hasStamina = false;
        dashTime = dashDuration;
        staminaRecoveryTimer = 0f;

        // Determine dash direction based on facing direction
        float dashDirection = transform.localScale.x;
        
        // Apply dash force
        body.linearVelocity = new Vector2(dashDirection * dashForce, 0);
        
        // Optional: Add visual effect for dash
        StartCoroutine(DashEffect());
    }

    private void HandleDash()
    {
        dashTime -= Time.deltaTime;
        
        if (dashTime <= 0)
        {
            isDashing = false;
            // Optional: Reduce velocity after dash ends
            body.linearVelocity = new Vector2(body.linearVelocity.x * 0.5f, body.linearVelocity.y);
        }
    }

    private IEnumerator DashEffect()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0f;
        Color[] dashColors = new Color[] { Color.blue, Color.red, Color.green };
        int colorIndex = 0;

        // Alternate colors during dash
        while (elapsedTime < dashDuration)
        {
            spriteRenderer.color = dashColors[colorIndex];
            colorIndex = (colorIndex + 1) % dashColors.Length;
            
            yield return new WaitForSeconds(dashDuration / 6); // Change color 6 times during dash
            elapsedTime += dashDuration / 6;
        }
        
        spriteRenderer.color = originalColor;
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpHeight);
        anim.SetTrigger("jump");
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        if (hit.collider != null)
        {
            hasDoubleJump = true; // Reset double jump when grounded
        }
        return hit.collider != null;
    }

    private bool onWall()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    }

    // Speed buffs (existing code)
    public void ApplySpeedBuff(float amount, float duration)
    {
        StartCoroutine(SpeedBuff(amount, duration));
    }

    private IEnumerator SpeedBuff(float amount, float duration)
    {
        speed += amount;
        yield return new WaitForSeconds(duration);
        speed -= amount;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}