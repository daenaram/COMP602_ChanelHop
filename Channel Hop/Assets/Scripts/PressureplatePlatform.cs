using UnityEngine;

public class PressureplatePlatform : MonoBehaviour
{
    public float speed = 2f;
    public Transform point1; // starting position
    public Transform point2; // target when plate is pressed
    public bool isActive = false; // controlled by the plate

    void Start()
    {
        if (point1 != null)
            transform.position = point1.position; // start at point1
        else
            Debug.LogWarning("Point1 not assigned!");
    }

    void Update()
    {
        if (isActive && point2 != null)
        {
            // Move toward point2 while plate is active
            transform.position = Vector2.MoveTowards(transform.position, point2.position, speed * Time.deltaTime);
        }
        else if (point1 != null)
        {
            // Return to point1 when plate is inactive
            transform.position = Vector2.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player1") || collision.collider.CompareTag("Player2"))
        {
            if (transform.position.y > collision.collider.transform.position.y - 0.8f)
                collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player1") || collision.collider.CompareTag("Player2"))
            collision.collider.transform.SetParent(null);
    }
}
