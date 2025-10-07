using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Vector3 originPos;
    public float pressDepth = 0.1f; // how far down it can move
    private bool moveBack = false;

    [Header("Platform Controlled by this Plate")]
    public PressureplatePlatform platform; // Assign the specific platform in Inspector

    void Start()
    {
        originPos = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.name == "Player1" || collision.transform.name == "Player2")
        {
            // Only trigger if the player is ABOVE the plate
            if (collision.transform.position.y > transform.position.y + 0.05f)
            {
                if (transform.position.y > originPos.y - pressDepth)
                {
                    transform.Translate(0, -0.01f, 0);
                }
                moveBack = false;

                // Activate the platform
                if (platform != null)
                    platform.isActive = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player1" || collision.transform.name == "Player2")
        {
            // Only change color if the player is actually above
            if (collision.transform.position.y > transform.position.y + 0.05f)
            {
                collision.transform.parent = transform;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name == "Player1" || collision.transform.name == "Player2")
        {
            moveBack = true;
            collision.transform.parent = null;
            GetComponent<SpriteRenderer>().color = Color.white;

            // Deactivate the platform so it returns
            if (platform != null)
                platform.isActive = false;
        }
    }

    void Update()
    {
        if (moveBack)
        {
            if (transform.position.y < originPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                moveBack = false;
            }
        }
    }
}
