using UnityEngine;

public class FloatingWeapon : MonoBehaviour
{
    public enum WeaponType { Sword, Axe, Staff, Bow }

    [SerializeField] private float amplitude = 0.5f;    
    [SerializeField] private float frequency = 1f;      
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private SpriteRenderer overlayRenderer; // Reference to child overlay renderer
    [SerializeField] private WeaponType weaponType; // Select in Inspector

    private Vector3 startPosition;
    private float timeOffset;
    private bool isInRange = false;
    private bool isCollected = false;

    private void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
        
        // Get reference to overlay renderer if not set
        if (overlayRenderer == null)
        {
            overlayRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        }
        
        // Hide overlay initially
        if (overlayRenderer != null)
        {
            overlayRenderer.enabled = false;
        }
    }

    private void Update()
    {
        if (isCollected) return;

        // Calculate bobbing motion
        float newY = startPosition.y + amplitude * Mathf.Sin((Time.time + timeOffset) * frequency);
        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );

        CheckPlayerRange();

        // Check for pickup
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectWeapon();
        }
    }

    private void CheckPlayerRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && overlayRenderer != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            bool wasInRange = isInRange;
            isInRange = distance <= interactionRange;

            // Toggle overlay visibility based on range
            if (wasInRange != isInRange)
            {
                overlayRenderer.enabled = isInRange;
            }
        }
    }

    private void CollectWeapon()
    {
        isCollected = true;
        if (overlayRenderer != null)
            overlayRenderer.enabled = false;

        // Hide the main sprite as well
        SpriteRenderer mainRenderer = GetComponent<SpriteRenderer>();
        if (mainRenderer != null)
            mainRenderer.enabled = false;

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

    public void ReactivateWeapon()
    {
        isCollected = false;
        if (overlayRenderer != null)
            overlayRenderer.enabled = false;
        
        SpriteRenderer mainRenderer = GetComponent<SpriteRenderer>();
        if (mainRenderer != null)
            mainRenderer.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
