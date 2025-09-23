using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    private string saveFileDirectory;          // Save folder path
    private string autoSaveFileName = "AutoSave.json"; // Autosave filename
    private float autoSaveInterval = 60f;      // Autosave every 60 seconds
    private bool isAutoSaveEnabled = true;     // Enable or disable autosave
    public static SaveData SaveDataToApply;    // Store save data between scenes

    // Awake runs before Start, ensures directory is set early
    private void Awake()
    {
        saveFileDirectory = Application.persistentDataPath;
    }

    // Start initializes autosave routine
    private void Start()
    {
        Debug.Log("Save directory location: " + saveFileDirectory);

        if (isAutoSaveEnabled)
        {
            StartCoroutine(AutoSaveRoutine());
        }
    }

    // Coroutine for periodic autosave
    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            SaveGame(autoSaveFileName);
            Debug.Log("Auto-saved game");
        }
    }

    // Save game to specified filename  
    public void SaveGame(string fileName)
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindWithTag("Player").transform.position,
            currentHealth = GameObject.FindWithTag("Player").GetComponent<Health>().currentHealth,
            currentWeapon = GameObject.FindWithTag("Player").GetComponent<PlayerWeaponController>().GetCurrentWeaponType(),
            currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            checkpointPosition = GameObject.FindWithTag("Player").GetComponent<PlayerRespawn>().currentCheckpointPosition() != null
                ? GameObject.FindWithTag("Player").GetComponent<PlayerRespawn>().currentCheckpointPosition().position
                : Vector3.zero
        };

        string filePath = Path.Combine(saveFileDirectory, fileName);
        File.WriteAllText(filePath, JsonUtility.ToJson(saveData));
        Debug.Log($"Game saved to: {filePath}");
    }

    // Load game from save file
    public void LoadGame(string fileName)
    {
        string filePath = Path.Combine(saveFileDirectory, fileName);
        if (File.Exists(filePath))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));

            SaveDataToApply = saveData; // store for applying after scene loads
            UnityEngine.SceneManagement.SceneManager.LoadScene(saveData.currentSceneName);
        }
    }

    // Get all save files in save directory
    public string[] GetSaveFiles()
    {
        return Directory.GetFiles(saveFileDirectory, "*.json");
    }

    // Resume game after pause
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
