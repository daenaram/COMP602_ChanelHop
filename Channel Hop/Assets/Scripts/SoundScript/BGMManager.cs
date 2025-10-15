using UnityEngine;

public class BGMManager : MonoBehaviour
{
    void Start()
    {
        if (MusicManager.Instance == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("OptionsMenu");
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
        }
        else
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            MusicManager.Instance.SetVolume(savedVolume);
        }
    }
}
