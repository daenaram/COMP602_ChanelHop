using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public void onStartClick()
    {
        SceneManager.LoadScene("Level1");
    }
    public void onExitClick()
    {
        Application.Quit();
    }
    public void onOptionsClick()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void onCreditsClick()
    {
        SceneManager.LoadScene("CreditsMenu");
    }
    public void onLoadGameClick()
    {
        SceneManager.LoadScene("LoadMenu");
    }
    public void onMultiplayerClick()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }
    public void onMuteClick()
    {
        AudioListener.volume = 0;
    }
}

