// CHANNEL HOP

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // Speed of the player's movement
    private Rigidbody2D body; // Reference to the RigidBody 2D component

    private void Awake() // Called everytime the script is loaded (game starts)
    {
        body = GetComponent<Rigidbody2D>(); // Get the RigidBody 2D component attached to this GameObject
    }

    private void Update() // Runs continuously every frame
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y); // // Move player left/right horizontally 
        
        if(Input.GetKey(KeyCode.Space))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed); // Move player up vertically when space is pressed
        }
    }
        
}
