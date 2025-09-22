using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveUI : MonoBehaviour
{
    [SerializeField] private GameObject savePanel;
    [SerializeField] private TMP_InputField saveNameInput;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button backButton;
    [SerializeField] private SaveController saveController;
    
    private PlayerMovement playerMovement; // Changed from PlayerInput to PlayerMovement
    private PlayerAttackingScript playerAttacking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        savePanel.SetActive(false);
        confirmButton.onClick.AddListener(OnConfirmSave);
        backButton.onClick.AddListener(OnBack);
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerAttacking = GameObject.FindWithTag("Player").GetComponent<PlayerAttackingScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowSaveDialog()
    {
        savePanel.SetActive(true);
        saveNameInput.text = "";
        saveNameInput.Select();
        Time.timeScale = 0;
        DisablePlayerInput();
    }

    private void OnConfirmSave()
    {
        if (!string.IsNullOrEmpty(saveNameInput.text))
        {
            saveController.SaveGame(saveNameInput.text + ".json");
            CloseSaveDialog();
        }
    }

    private void OnBack()
    {
        CloseSaveDialog();
    }

    private void CloseSaveDialog()
    {
        savePanel.SetActive(false);
        Time.timeScale = 1;
        EnablePlayerInput();
        saveController.ResumeGame();
    }

    private void DisablePlayerInput()
    {
        if (playerAttacking != null)
        {
            playerAttacking.enabled = false;
        }
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    private void EnablePlayerInput()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
        if (playerAttacking != null)
        {
            playerAttacking.enabled = true;
        }
    }

}
