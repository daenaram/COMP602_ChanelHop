using UnityEngine;
using UnityEngine.UI;

public class GameSFXManager : MonoBehaviour
{
    [SerializeField] private Slider sfxVolumeSlider;
    private SFXManager sfxManager;

    private void Start()
    {
        sfxManager = SFXManager.instance;
        
        if (sfxVolumeSlider != null && sfxManager != null)
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            sfxVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    private void OnVolumeChanged(float volume)
    {
        if (sfxManager != null)
        {
            sfxManager.SetVolume(volume);
        }
    }
}
