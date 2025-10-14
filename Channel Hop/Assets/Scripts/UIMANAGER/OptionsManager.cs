using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = MusicManager.Instance;
        
        if (musicVolumeSlider != null && musicManager != null)
        {
            // Set initial slider value to current volume
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            
            // Add listener for slider value changes
            musicVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    private void OnVolumeChanged(float volume)
    {
        if (musicManager != null)
        {
            musicManager.SetVolume(volume);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
