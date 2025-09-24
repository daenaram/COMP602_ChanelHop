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
    var p1 = GameObject.FindWithTag("Player1");
    var p2 = GameObject.FindWithTag("Player2");

    if (p1 == null || p2 == null)
    {
        Debug.LogWarning("Save failed: Player1 or Player2 not found.");
        return;
    }

    var p1Respawn = p1.GetComponent<PlayerRespawn>();
    var p2Respawn = p2.GetComponent<PlayerRespawn>();

    SaveData saveData = new SaveData()
    {
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,

        playerPosition1 = p1.transform.position,
        currentHealth1 = p1.GetComponent<Health>().currentHealth,
        currentWeapon1 = p1.GetComponent<PlayerWeaponController>().GetCurrentWeaponType(),
        checkpointPosition1 = p1Respawn.currentCheckpointPosition() != null
            ? p1Respawn.currentCheckpointPosition().position
            : Vector3.zero,

        playerPosition2 = p2.transform.position,
        currentHealth2 = p2.GetComponent<Health>().currentHealth,
        currentWeapon2 = p2.GetComponent<PlayerWeaponController>().GetCurrentWeaponType(),
        checkpointPosition2 = p2Respawn.currentCheckpointPosition() != null
            ? p2Respawn.currentCheckpointPosition().position
            : Vector3.zero
    };

    string filePath = Path.Combine(saveFileDirectory, fileName);
    File.WriteAllText(filePath, JsonUtility.ToJson(saveData, true)); // pretty-print for debugging
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
