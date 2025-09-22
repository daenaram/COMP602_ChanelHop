using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class LoadUI : MonoBehaviour
{
    [SerializeField] private Transform saveListContainer; // Container for save buttons
    [SerializeField] private Button saveButtonTemplate;   // Template button for saves
    [SerializeField] private Button backButton;           // Button to go back to menu
    [SerializeField] private SaveController saveController; // Reference to SaveController

    private void Start()
    {
        saveButtonTemplate.gameObject.SetActive(false); // Hide template
        backButton.onClick.AddListener(OnBack);         // Back button listener

        PopulateSaveList();                             // Create save buttons
    }

    // Populate the save list with buttons
    private void PopulateSaveList()
    {
        // Remove any old buttons except template
        foreach (Transform child in saveListContainer)
        {
            if (child != saveButtonTemplate.transform)
                Destroy(child.gameObject);
        }

        // Get all save files
        string[] saveFiles = saveController.GetSaveFiles();

        // Create a button for each save
        foreach (string filePath in saveFiles)
        {
            string fileName = Path.GetFileName(filePath);

            Button newButton = Instantiate(saveButtonTemplate, saveListContainer);
            newButton.gameObject.SetActive(true);

            // Set button text to file name
            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null) buttonText.text = fileName;

            // Add click listener to load the save
            newButton.onClick.AddListener(() => saveController.LoadGame(fileName));
        }
    }

    // Go back to the main menu
    private void OnBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
