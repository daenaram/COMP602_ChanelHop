// CHANNEL HOP

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // Speed of the player's movement
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body; // Reference to the RigidBody 2D component
    private Animator anim;
    private BoxCollider2D boxCollider; // Reference to the BoxCollider2D component
    private float wallJumpCooldown;

    private void Awake() // Called everytime the script is loaded (game starts)
    {
        //Grabs references for rigidbody and animator components
        body = GetComponent<Rigidbody2D>(); // Get the RigidBody 2D component attached to this GameObject
        anim = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() // Runs continuously every frame
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input from the player (A/D keys or Left/Right arrows)

        //Flip the player sprite based on the direction of movement
        if (horizontalInput > 0.01f)
        {
          transform.localScale = new Vector3(1, 1, 1); // Face right when moving right. one on the x-axis, one on the y-axis, one on the z-axis
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left when moving left. one on the x-axis, one on the y-axis, one on the z-axis
        }

        //Set animator parameters based on player input
        anim.SetBool("run", horizontalInput != 0); //is one equal to zero? If not, set run to true else false
        anim.SetBool("grounded", isGrounded()); // Set grounded parameter in animator based on the grounded variable

        /* TESTING - console log the result of isGrounded and onWall
        print(onWall());
        */

        // Wall jump logic
        if(wallJumpCooldown < 0.2f)
        {
            if (Input.GetKey(KeyCode.Space) && isGrounded())
            {
                Jump(); // Call the Jump method when the space key is pressed
            }

            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y); // // Move player left/right horizontally 

            if(onWall() && !isGrounded())
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
            {
                body.gravityScale = 1;
            }
        }

        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed); // Move player up vertically when space is pressed
        anim.SetTrigger("jump"); // Trigger the jump animation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")// Ground is the tag assigned to the ground GameObject
        {
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer); // Uses ray casting to fire virtual laser in a certain direction
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer); // Uses ray casting to fire virtual laser in a certain direction
        return raycastHit.collider != null;
    }

}
