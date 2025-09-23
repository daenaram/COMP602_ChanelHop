using UnityEngine;


/// Controls floating weapon pickups that bob up and down and can be collected by the player

public class FloatingWeapon : MonoBehaviour
{
    // Defines the available types of weapons that can be picked up
    public enum WeaponType {  Sword, Axe, Staff, Bow, None }

    // Movement and interaction settings
    [SerializeField] private float amplitude = 0.5f;    // Height of bobbing motion
    [SerializeField] private float frequency = 1f;      // Speed of bobbing motion
    [SerializeField] private float interactionRange = 2f;  // Distance at which player can interact
    [SerializeField] private SpriteRenderer overlayRenderer;  // Visual indicator when in range
    [SerializeField] private WeaponType weaponType;  // Type of weapon this pickup represents

    // Internal state tracking
    private Vector3 startPosition;  // Original position of the weapon
    private float timeOffset;  // Random offset to prevent all weapons bobbing in sync
    private bool isInRange = false;  // Whether player is in pickup range
    private bool isCollected = false;  // Whether weapon has been collected

    private void Start()
    {
        // Store initial position and add random time offset
        startPosition = transform.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
        
        // Setup overlay renderer for interaction indicator
        if (overlayRenderer == null)
        {
            overlayRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        }
        
        if (overlayRenderer != null)
        {
            overlayRenderer.enabled = false;
        }
    }

    private void Update()
    {
        // Skip updates if weapon is already collected
        if (isCollected) return;

        // Apply bobbing motion using sine wave
        float newY = startPosition.y + amplitude * Mathf.Sin((Time.time + timeOffset) * frequency);
        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );

        // Check if player is in range and handle pickup input
        CheckPlayerRange();
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectWeapon();
        }
    }

    private void CheckPlayerRange()
    {
        // Find player and check distance
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && overlayRenderer != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            bool wasInRange = isInRange;
            isInRange = distance <= interactionRange;

            // Show/hide interaction indicator when entering/leaving range
            if (wasInRange != isInRange)
            {
                overlayRenderer.enabled = isInRange;
                Debug.Log("Player is " + (isInRange ? "in" : "out of") + " range of weapon: " + weaponType);
            }
        }
    }

    private void CollectWeapon()
    {
        // Mark as collected and hide visual elements
        isCollected = true;
        if (overlayRenderer != null)
            overlayRenderer.enabled = false;

        SpriteRenderer mainRenderer = GetComponent<SpriteRenderer>();
        if (mainRenderer != null)
            mainRenderer.enabled = false;

        // Find player and attempt to equip weapon
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var weaponController = player.GetComponent<PlayerWeaponController>();
            if (weaponController != null)
            {
                print("Equipping weapon: " + weaponType);
                weaponController.EquipWeapon(weaponType, this);
            }
        }
    }

    // Called when player switches to a different weapon to make this one available again
    public void ReactivateWeapon()
    {
        isCollected = false;
        if (overlayRenderer != null)
            overlayRenderer.enabled = false;
        
        SpriteRenderer mainRenderer = GetComponent<SpriteRenderer>();
        if (mainRenderer != null)
            mainRenderer.enabled = true;
    }

    // Visualize interaction range in the Unity editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
