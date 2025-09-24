using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = Vector3.zero;

    [SerializeField] private Camera cam;
    [SerializeField] private float minZoom = 5f;    
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float zoomLimiter = 10f;



    private Vector3 velocity = Vector3.zero; 

    private void LateUpdate() // Runs continuously every frame
    {
        if (player1 == null || player2 == null)
            return;

        Vector3 midpoint = (player1.position + player2.position) / 2f;// Calculate the midpoint between the two players
        Vector3 desiredPosition = midpoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        float distance = Vector3.Distance(player1.position, player2.position);
        float targetZoom = Mathf.Lerp(minZoom, maxZoom, Mathf.Clamp01(distance / zoomLimiter));// Adjust camera zoom based on player distance

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime);

    }


}
