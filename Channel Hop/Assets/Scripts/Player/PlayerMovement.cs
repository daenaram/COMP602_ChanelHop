using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum Player { Player1, Player2 }
    public Player playerID = Player.Player1; // assign in Inspector per player

    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    // add reference to player1 and max distance player2 can move
    [SerializeField] private Transform player1; // assign Player 1 in Inspector
    [SerializeField] private float maxDistance = 8f; // max horizontal distance Player 2 can move from Player 1

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

        // Assign controls based on player
        if (playerID == Player.Player1)
        {
            horizontalInput = (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0);
            jumpInput = Input.GetKey(KeyCode.W);
        }
        else if (playerID == Player.Player2)
        {
            horizontalInput = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
            jumpInput = Input.GetKey(KeyCode.UpArrow);
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
            if (jumpInput && isGrounded())
                Jump();

            body.linearVelocity = new Vector2(horizontalInput * speed, body.velocity.y);

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
                    body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Lerp(body.linearVelocity.y, -1f, Time.deltaTime*10)); // Stop vertical movement when on wall and no horizontal input
                }
                
            }
            else
                body.gravityScale = 1;
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        // clamp player2 - prevents moving too fat from player1
        if (playerID == Player.Player2 && player1 != null)
        {
            float distance = transform.position.x - player1.position.x;

            if (distance > maxDistance)
                transform.position = new Vector3(player1.position.x + maxDistance, transform.position.y, transform.position.z);
            else if (distance < -maxDistance)
                transform.position = new Vector3(player1.position.x - maxDistance, transform.position.y, transform.position.z);
        }

    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        anim.SetTrigger("jump");
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
    }

    private bool onWall()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    }
}
