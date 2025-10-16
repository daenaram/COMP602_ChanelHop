using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    public AudioClip menuMusic;    // BGM for menu scenes
    public AudioClip gameMusic;    // BGM for gameplay scenes
    [SerializeField] private Slider musicVolumeSlider;

    private const string VolumeKey = "MusicVolume";
    private readonly string[] menuScenes = { "Menu", "LoadMenu", "OptionsMenu" };

    // Sets up the singleton and loads volume settings
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
            // Syncs the slider with existing instance and destroys duplicates
            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = Instance.audioSource.volume;
            }
            Destroy(gameObject);
        }
    }

    // Runs once at the start; plays background music and sets up the volume slider
    void Start()
    {
        // Subscribe to scene changes
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;
        
        // Play initial music based on current scene
        PlayMusicForCurrentScene();

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = audioSource.volume;
            musicVolumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // Changes the volume based on the slider and saves it
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        SaveVolumeSettings();
    }

    // Loads saved volume settings from PlayerPrefs
    private void LoadVolumeSettings()
    {
        if (audioSource != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
            audioSource.volume = savedVolume;
        }
    }

    // Saves the current volume setting to PlayerPrefs
    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(VolumeKey, audioSource.volume);
        PlayerPrefs.Save();
    }

    // Plays the given music clip (or the current one if none is given)
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

    // Pauses the currently playing background music
    public void PauseBackgroundMusic()
    {
        audioSource.Pause();
    }

    // Resumes the paused background music
    public void ResumeBackgroundMusic()
    {
        audioSource.UnPause();
    }

    // Toggles between pausing and resuming the background music
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

    // Checks if the MusicManager is properly initialized
    public bool IsInitialized()
    {
        return audioSource != null && Instance == this;
    }

    // Handles scene change events
    private void OnSceneChanged(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
    {
        PlayMusicForCurrentScene();
    }

    // Plays the appropriate music for the current scene
    private void PlayMusicForCurrentScene()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        bool isMenuScene = System.Array.Exists(menuScenes, scene => scene == currentSceneName);
        
        // Check if we need to change the music
        bool shouldChangeMusic = (isMenuScene && audioSource.clip != menuMusic) || 
                               (!isMenuScene && audioSource.clip != gameMusic);
        
        if (shouldChangeMusic)
        {
            PlayMusic(true, isMenuScene ? menuMusic : gameMusic);
        }
    }
}
