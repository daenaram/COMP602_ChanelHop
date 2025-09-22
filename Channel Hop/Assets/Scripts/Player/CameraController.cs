using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float lookAheadDistance; // Distance the camera looks ahead of the player
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float groundRayLength = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float yOffSet;

    // Speed at which the camera moves
    private float lookAhead; // Current look ahead distance
    private bool isGrounded; // Whether the player is on the ground
    private Vector3 velocity = Vector3.zero; 

    private void Update() // Runs continuously every frame
    {
        isGrounded = IsPlayerGrounded();

        float verticalPosition = Mathf.Lerp(transform.position.y, player.position.y + yOffSet, Time.deltaTime * cameraSpeed);


        transform.position = new Vector3(player.position.x + lookAhead, verticalPosition, transform.position.z);


        lookAhead = Mathf.Lerp(lookAhead, (lookAheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed); // [Change 4]

        // time.deltatime makes it frame rate independent

    }
    private bool IsPlayerGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, groundRayLength, groundLayer);
        return hit.collider != null;
    }


}
