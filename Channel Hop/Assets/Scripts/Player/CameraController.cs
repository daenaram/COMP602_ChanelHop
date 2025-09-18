using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player; // assigned to player 1
    [SerializeField] private float lookAheadDistance; // Distance the camera looks ahead of the player
    [SerializeField] private float cameraSpeed; // Speed at which the camera moves
    private float lookAhead; // Current look ahead distance
    private void Update() // Runs continuously every frame
    {
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead,(lookAheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        // time.deltatime makes it frame rate independent

    }
    

}
