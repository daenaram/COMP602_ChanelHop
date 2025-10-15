using UnityEngine;
using UnityEngine.UI;

public class GameVolumeManager : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = MusicManager.Instance;
        
        if (musicVolumeSlider != null && musicManager != null)
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
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
}
