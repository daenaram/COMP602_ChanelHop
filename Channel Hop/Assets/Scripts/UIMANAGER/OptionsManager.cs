using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;   
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = MusicManager.Instance;
        
        // Setup music volume
        if (musicVolumeSlider != null && musicManager != null)
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        // Setup SFX volume
        if (sfxVolumeSlider != null && SFXManager.instance != null)
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }
    }

    private void OnMusicVolumeChanged(float volume)
    {
        if (musicManager != null)
        {
            musicManager.SetVolume(volume);
        }
    }

    private void OnSFXVolumeChanged(float volume)
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.SetVolume(volume);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
