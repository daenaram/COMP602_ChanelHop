// CHANNEL HOP

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // Speed of the player's movement
    private Rigidbody2D body; // Reference to the RigidBody 2D component
    private Animator anim;
    private bool grounded;

    private void Awake() // Called everytime the script is loaded (game starts)
    {
        //Grabs references for rigidbody and animator components
        body = GetComponent<Rigidbody2D>(); // Get the RigidBody 2D component attached to this GameObject
        anim = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
    }

    private void Update() // Runs continuously every frame
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input from the player (A/D keys or Left/Right arrows)
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y); // // Move player left/right horizontally 


        //Flip the player sprite based on the direction of movement
        if (horizontalInput > 0.01f)
        {
          transform.localScale = new Vector3(1, 1, 1); // Face right when moving right. one on the x-axis, one on the y-axis, one on the z-axis
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left when moving left. one on the x-axis, one on the y-axis, one on the z-axis
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump(); // Call the Jump method when the space key is pressed
        }

        //Set animator parameters based on player input
        anim.SetBool("run", horizontalInput != 0); //is one equal to zero? If not, set run to true else false
        anim.SetBool("grounded", grounded); // Set grounded parameter in animator based on the grounded variable
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed); // Move player up vertically when space is pressed
        anim.SetTrigger("jump"); // Trigger the jump animation
        grounded = false; // Set grounded to false when jumping
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")// Ground is the tag assigned to the ground GameObject
        {
            grounded = true; // Set grounded to true when colliding with the ground
        }
    }

}
