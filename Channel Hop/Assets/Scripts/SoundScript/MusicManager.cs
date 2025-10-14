using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    [SerializeField] private Slider musicVolumeSlider;

    private const string VolumeKey = "MusicVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            LoadVolumeSettings();
        }
        else
        {
            // Update slider if we return to options scene
            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = Instance.audioSource.volume;
            }
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayMusic(false, backgroundMusic);
        }

        if (musicVolumeSlider != null)
        {
            // Set initial slider value and add listener
            musicVolumeSlider.value = audioSource.volume;
            musicVolumeSlider.onValueChanged.AddListener(SetVolume);
        }
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
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
            audioSource.volume = savedVolume;
        }
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(VolumeKey, audioSource.volume);
        PlayerPrefs.Save();
    }

    public void PlayMusic(bool resetSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        if (audioSource.clip != null)
        {
            if (resetSong)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }

    public void PauseBackgroundMusic()
    {
        audioSource.Pause();
    }

    public void ResumeBackgroundMusic()
    {
        audioSource.UnPause();
    }

    public void BGMToggle()
    {
        if (audioSource.isPlaying)
        {
            PauseBackgroundMusic();
        }
        else
        {
            ResumeBackgroundMusic();
        }
    }
    public bool IsInitialized()
    {
        return audioSource != null && Instance == this;
    }
}
