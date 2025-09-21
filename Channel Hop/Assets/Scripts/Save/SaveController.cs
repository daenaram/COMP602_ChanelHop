using UnityEngine;
using System.IO;

public class SaveController : MonoBehaviour
{   
    private string saveFileName = "SaveFile.json";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveFileName = Path.Combine(Application.persistentDataPath, saveFileName);
        Debug.Log("Save file location: " + saveFileName);
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindWithTag("Player").transform.position,
            currentHealth = GameObject.FindWithTag("Player").GetComponent<Health>().currentHealth,
            currentWeapon = GameObject.FindWithTag("Player").GetComponent<PlayerWeaponController>().GetCurrentWeaponType(),
            currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        };
        File.WriteAllText(saveFileName, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if(File.Exists(saveFileName))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveFileName));

            GameObject.FindWithTag("Player").transform.position = saveData.playerPosition;
            GameObject.FindWithTag("Player").GetComponent<Health>().SetHealth(saveData.currentHealth); // Changed this line
            GameObject.FindWithTag("Player").GetComponent<PlayerWeaponController>().EquipWeapon(saveData.currentWeapon, null);
            UnityEngine.SceneManagement.SceneManager.LoadScene(saveData.currentSceneName);
        }
        else
        {
            SaveGame(); // Create a new save file if none exists
        }
    }
}
