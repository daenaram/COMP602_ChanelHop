using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance { get; private set; }
    private AudioSource audioSource;
    [SerializeField] private Slider sfxVolumeSlider;

    private const string SFXVolumeKey = "SFXVolume";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            LoadVolumeSettings();
        }
        else
        {
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = instance.audioSource.volume;
            }
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = audioSource.volume;
            sfxVolumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        SaveVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        if (audioSource != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
            audioSource.volume = savedVolume;
        }
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(SFXVolumeKey, audioSource.volume);
        PlayerPrefs.Save();
    }
}
