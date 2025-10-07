using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    [SerializeField] private Slider musicVolumeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayMusic(false, backgroundMusic);
        }
        musicVolumeSlider.onValueChanged.AddListener(delegate { setVolume(musicVolumeSlider.value); });
    }
    public static void setVolume(float volume)
    {
        Instance.audioSource.volume = volume;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
}
