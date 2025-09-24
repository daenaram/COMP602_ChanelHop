using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; //sound that play when picking up a new checkpoint
    private static Transform currentCheckpoint; //we'll store our last checkpoint here
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }
    public void Respawn()
    {

        transform.position = currentCheckpoint.position;// Move player to checkpoint position
        playerHealth.Respawn();//restore player health
        Debug.Log($"Respawning {gameObject.name} at {transform.position}");

        //if (currentCheckpoint != null)
        //{
        //    transform.position = currentCheckpoint.position;// Move player to checkpoint position
        //    playerHealth.Respawn();//restor player health

        //}
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // store checkpoint we activated as current checkpoint
            //SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear");// Trigger checkpoint animation using appear trigger
        }
    }

    public Transform currentCheckpointPosition()
    {
        return currentCheckpoint;
    }
    public void SetCheckpointPosition(Vector3 pos)
{
    if (pos != Vector3.zero) // avoid default "no checkpoint"
    {
        GameObject checkpointObj = new GameObject("LoadedCheckpoint");
        checkpointObj.transform.position = pos;
        currentCheckpoint = checkpointObj.transform;
    }
}

}
