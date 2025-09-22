using UnityEngine;
using UnityEngine.UI;


public class PlayAgainScript : MonoBehaviour

{
    [SerializeField] private GameObject playAgain;
    [SerializeField] private Button reviveButton;
    [SerializeField] private Button exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playAgain.SetActive(false); // Ensure the pause menu is inactive at the start
        reviveButton.interactable = true; // Ensure the pause button is interactable
        exitButton.interactable = true; // Ensure the pause button is interactable

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showPlayAgain()
    {
        playAgain.SetActive(true); // Activate the pause menu
        reviveButton.interactable = true; // enable the revive button
        exitButton.interactable = true; // enable the exit button
    }

    public void revive()
    {
        Debug.Log("reviving");
    }

    public void exit()
    {
        Debug.Log("exiting");
    }
}
