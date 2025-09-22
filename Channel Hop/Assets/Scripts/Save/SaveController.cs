using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{   
    private string saveFileDirectory;
    private string autoSaveFileName = "AutoSave.json";
    private float autoSaveInterval = 60f; // Autosave every 60 seconds
    private bool isAutoSaveEnabled = true;

    void Start()
    {
        saveFileDirectory = Application.persistentDataPath;
        Debug.Log("Save directory location: " + saveFileDirectory);
        
        if (isAutoSaveEnabled)
        {
            StartCoroutine(AutoSaveRoutine());
        }
    }

    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            SaveGame(autoSaveFileName);
            Debug.Log("Auto-saved game");
        }
    }

    public void SaveGameWithName()
    {
        // This should be called by your save button
        StartCoroutine(ShowSaveDialog());
    }

    private IEnumerator ShowSaveDialog()
    {
        Time.timeScale = 0;
        // This will be handled by SaveUI now
        yield break;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    // Change from private to public
    public void SaveGame(string fileName)
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindWithTag("Player").transform.position,
            currentHealth = GameObject.FindWithTag("Player").GetComponent<Health>().currentHealth,
            currentWeapon = GameObject.FindWithTag("Player").GetComponent<PlayerWeaponController>().GetCurrentWeaponType(),
            currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        };

        string filePath = Path.Combine(saveFileDirectory, fileName);
        File.WriteAllText(filePath, JsonUtility.ToJson(saveData));
        Debug.Log($"Game saved to: {filePath}");
    }

    public void LoadGame(string fileName)
    {
        string filePath = Path.Combine(saveFileDirectory, fileName);
        if(File.Exists(filePath))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));

            GameObject.FindWithTag("Player").transform.position = saveData.playerPosition;
            GameObject.FindWithTag("Player").GetComponent<Health>().SetHealth(saveData.currentHealth);
            GameObject.FindWithTag("Player").GetComponent<PlayerWeaponController>().EquipWeapon(saveData.currentWeapon, null);
            UnityEngine.SceneManagement.SceneManager.LoadScene(saveData.currentSceneName);
        }
    }

    public string[] GetSaveFiles()
    {
        return Directory.GetFiles(saveFileDirectory, "*.json");
    }
}
