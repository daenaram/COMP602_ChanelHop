using UnityEngine;

/// Controls floating weapon pickups that bob up and down and can be collected by Player1 or Player2
public class FloatingWeapon : MonoBehaviour
{
    public enum WeaponType { None, Sword, Axe, Staff, Bow }

    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private SpriteRenderer overlayRenderer;
    [SerializeField] private WeaponType weaponType;

    private Vector3 startPosition;
    private float timeOffset;

    private GameObject player1;
    private GameObject player2;
    private bool isCollected = false; // Prevents two players taking it simultaneously

    private void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);

        if (overlayRenderer == null)
            overlayRenderer = transform.GetComponentInChildren<SpriteRenderer>();

        if (overlayRenderer != null)
            overlayRenderer.enabled = false;

        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }

    private void Update()
    {
        if (isCollected) return;

        float newY = startPosition.y + amplitude * Mathf.Sin((Time.time + timeOffset) * frequency);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        CheckPlayerRange(player1, KeyCode.S);
        CheckPlayerRange(player2, KeyCode.DownArrow);
    }

    private void CheckPlayerRange(GameObject player, KeyCode pickupKey)
    {
        if (player == null || overlayRenderer == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool inRange = distance <= interactionRange;

        if (inRange)
        {
            overlayRenderer.enabled = true;

            if (Input.GetKeyDown(pickupKey))
            {
                CollectWeapon(player);
            }
        }
        else if (!AnyPlayerInRange())
        {
            overlayRenderer.enabled = false;
        }
    }

    private bool AnyPlayerInRange()
    {
        if (player1 != null && Vector3.Distance(transform.position, player1.transform.position) <= interactionRange) return true;
        if (player2 != null && Vector3.Distance(transform.position, player2.transform.position) <= interactionRange) return true;
        return false;
    }

    private void CollectWeapon(GameObject player)
    {
        var weaponController = player.GetComponent<PlayerWeaponController>();
        if (weaponController != null && !isCollected)
        {
            Debug.Log($"{player.name} equipped: {weaponType}");
            
            // Get previous weapon before equipping new one
            FloatingWeapon previousWeapon = weaponController.GetCurrentWeapon();
            
            // If player had a weapon, reactivate it
            if (previousWeapon != null)
            {
                previousWeapon.ReactivateWeapon();
            }

            // Equip the new weapon
            weaponController.EquipWeapon(weaponType, this);

            // Mark as collected and hide sprite
            isCollected = true;
            if (overlayRenderer != null) overlayRenderer.enabled = false;
            SpriteRenderer mainRenderer = GetComponent<SpriteRenderer>();
            if (mainRenderer != null) mainRenderer.enabled = false;
        }
    }

    // Called when player drops weapon so another can pick it up
    public void ReactivateWeapon()
    {
        isCollected = false;
        
        // Reset position to starting position
        transform.position = new Vector3(
            transform.position.x,
            startPosition.y,
            transform.position.z
        );
        
        if (overlayRenderer != null) 
        {
            overlayRenderer.enabled = false;
        }
        
        SpriteRenderer mainRenderer = GetComponent<SpriteRenderer>();
        if (mainRenderer != null) 
        {
            mainRenderer.enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
