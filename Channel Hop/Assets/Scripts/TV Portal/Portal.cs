using UnityEngine;
using UnityEngine.SceneManagement; // required for scene loading

public class Portal : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // name of next scene (e.g. "Level2")

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player touched the portal
        if (collision.CompareTag("Player1")||collision.CompareTag("Player2"))
        {
            Debug.Log("A player entered portal, loading " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
