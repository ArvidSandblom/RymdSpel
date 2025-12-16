using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject playerSelection;
    private shipCarry leaderboard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leaderboard = FindAnyObjectByType<shipCarry>();

        
        if (playerSelection != null)
        {
            playerSelection.SetActive(false);
        }
        
    }
    public void playGame()
    {
        // Capture username from InputField before starting game
        if (leaderboard != null && leaderboard.usernameInputField != null)
        {
            leaderboard.SetUsernameFromInput(leaderboard.usernameInputField.text);
        }
        SceneManager.LoadScene(1);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void shipSelection()
    {
        SceneManager.LoadScene(3);
    }
    public void menuSwitch()
    {        
        int index = 0;
        if (index == 0)
        {
            if (menu != null) menu.SetActive(false);
            if (playerSelection != null) playerSelection.SetActive(true);
            index = 1;
        }
        else if (index == 1)
        {
            
            if (menu != null) menu.SetActive(true);
            if (playerSelection != null) playerSelection.SetActive(false);
            index = 0;
        }
    }
}
