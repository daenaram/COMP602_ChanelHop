using UnityEngine;

public class Followplayer : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float bobFrequency = 5f;
    [SerializeField] private float bobAmplitude = 0.1f;
    [SerializeField] private float rotationSpeed = 45f;
    
    private Vector3 startPosition;
    private float bobTime = 0f;
    private Animator playerAnimator; // Reference to player's animator if needed
    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.localPosition;
        playerAnimator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player's horizontal input to check if moving
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isMoving = Mathf.Abs(horizontalInput) > 0.1f;

        if (isMoving)
        {
            // Running animation movement
            RunningMovement();
        }
        else
        {
            // Idle animation movement
            IdleMovement();
        }
    }

    private void RunningMovement()
    {
        bobTime += Time.deltaTime * bobFrequency;
        
        // Calculate bob motion
        float bobOffset = Mathf.Sin(bobTime) * bobAmplitude;
        
        // Apply bob motion to weapon position
        transform.localPosition = startPosition + new Vector3(0f, bobOffset, 0f);
        
        // Add slight rotation sway
        float rotationOffset = Mathf.Sin(bobTime) * 15f;
        transform.localRotation = Quaternion.Euler(0f, 0f, rotationOffset);
    }

    private void IdleMovement()
    {
        bobTime += Time.deltaTime * (bobFrequency * 0.5f); // Slower frequency for idle
        
        // Smaller, gentler bob motion for idle
        float bobOffset = Mathf.Sin(bobTime) * (bobAmplitude * 0.3f);
        
        // Apply subtle bob motion
        transform.localPosition = startPosition + new Vector3(0f, bobOffset, 0f);
        
        // Subtle rotation
        float rotationOffset = Mathf.Sin(bobTime) * 5f;
        transform.localRotation = Quaternion.Euler(0f, 0f, rotationOffset);
    }
}
