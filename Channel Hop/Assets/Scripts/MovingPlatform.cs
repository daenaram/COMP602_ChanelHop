using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startPoint; // Index of the starting point in the points array
    public Transform[] points; // Array of points to move between
    private int i;

    private
    void Start()
    {
        transform.position = points[startPoint].position;//Set initial position to the starting point
        Debug.Log("Starting at point: " + startPoint);
    }

    
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);// Move towards the next point
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player1") || collision.collider.CompareTag("Player2"))
            if (transform.position.y > collision.collider.transform.position.y - 0.8f) // Only parent the player if they are on top of the platform and not colliding from the side
                collision.collider.transform.SetParent(transform); // Make the player a child of the platform so the player moves with it
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);// Unparent the player when they leave the platform so they don't move with it 
    }
}
