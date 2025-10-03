using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
