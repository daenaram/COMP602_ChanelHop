using UnityEngine;

public class BGMManager : MonoBehaviour
{
    void Start()
    {
        // Check if MusicManager exists in scene
        if (MusicManager.Instance == null)
        {
            // Load the Options scene first to initialize MusicManager
            UnityEngine.SceneManagement.SceneManager.LoadScene("OptionsMenu");
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
        }
        else
        {
            // Apply saved volume settings
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            MusicManager.Instance.SetVolume(savedVolume);
        }
    }
}
