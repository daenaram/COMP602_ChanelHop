using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform prevRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController2 cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            float targetX;

            if (collision.transform.position.x < transform.position.x)
            {
                targetX = nextRoom.position.x;
                cam.MoveToNewRoom(nextRoom);
            }
            else
            {
                targetX = prevRoom.position.x;
                cam.MoveToNewRoom(prevRoom);
            }

            Debug.Log($"Camera current X: {cam.transform.position.x}, Target X: {targetX}");
        }
    }
}

