using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveUI : MonoBehaviour
{
    [SerializeField] private GameObject savePanel;       // Panel for save dialog
    [SerializeField] private TMP_InputField saveNameInput; // Input field for save name
    [SerializeField] private Button confirmButton;       // Button to confirm save
    [SerializeField] private Button backButton;          // Button to cancel save
    [SerializeField] private SaveController saveController; // Reference to SaveController
    
    private PlayerMovement playerMovement;               // Reference to player movement
    private PlayerAttackingScript playerAttacking;      // Reference to player attack script

    // Initialize references and button listeners
    void Start()
    {
        savePanel.SetActive(false); // Hide panel at start
        confirmButton.onClick.AddListener(OnConfirmSave);
        backButton.onClick.AddListener(OnBack);

        playerMovement = GameObject.FindWithTag("Player1").GetComponent<PlayerMovement>();
        playerAttacking = GameObject.FindWithTag("Player1").GetComponent<PlayerAttackingScript>();
        playerMovement = GameObject.FindWithTag("Player2").GetComponent<PlayerMovement>();
        playerAttacking = GameObject.FindWithTag("Player2").GetComponent<PlayerAttackingScript>();
    }

    void Update()
    {
        // Not used, can remove if not needed
    }

    // Show save panel and disable player input
    public void ShowSaveDialog()
    {
        savePanel.SetActive(true);
        saveNameInput.text = "";
        saveNameInput.Select();
        Time.timeScale = 0; // Pause the game
        DisablePlayerInput();
    }

    // Called when confirm button is pressed
    public void OnConfirmSave()
    {
        if (!string.IsNullOrEmpty(saveNameInput.text))
        {
            saveController.SaveGame(saveNameInput.text + ".json");
            CloseSaveDialog();
        }
    }

    // Called when back button is pressed
    private void OnBack()
    {
        CloseSaveDialog();
    }

    // Close save panel and resume player input
    public void CloseSaveDialog()
    {
        savePanel.SetActive(false);
        Time.timeScale = 1; // Resume game
        EnablePlayerInput();
        saveController.ResumeGame();
    }

    // Disable player movement and attacking
    private void DisablePlayerInput()
    {
        if (playerAttacking != null)
            playerAttacking.enabled = false;

        if (playerMovement != null)
            playerMovement.enabled = false;
    }

    // Enable player movement and attacking
    private void EnablePlayerInput()
    {
        if (playerMovement != null)
            playerMovement.enabled = true;

        if (playerAttacking != null)
            playerAttacking.enabled = true;
    }
}
