using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private Vector3 offset = Vector3.zero; // adjustable in inspector

    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPos = new Vector3(currentPosX, transform.position.y, transform.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, speed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
