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

            if (collision.transform.position.x < transform.position.x)//if player is on the left side of the door, go to next room
            {
                targetX = nextRoom.position.x;//set targetX to next room's x position
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

