using UnityEngine;

public class Wall : MonoBehaviour
{
    public float speed = 2f;
    public Transform point1; // starting position
    public Transform point2; // target when active
    public bool isActive = false; // controlled by the plate

    void Start()
    {
        if (point1 != null)
            transform.position = point1.position; // start at point1
        
    }

    void Update()
    {
        if (isActive && point2 != null)
        {
            // Move toward point2 when active
            transform.position = Vector2.MoveTowards(transform.position, point2.position, speed * Time.deltaTime);
        }
        else if (point1 != null)
        {
            // Return to point1 when inactive
            transform.position = Vector2.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Might add effects or damage to player here
        if (collision.collider.CompareTag("Player1") || collision.collider.CompareTag("Player2"))
        {
            
            Debug.Log("Player hit the moving wall!");
        }
    }
}
