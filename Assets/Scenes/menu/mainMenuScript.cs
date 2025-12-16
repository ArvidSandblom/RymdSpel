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

        if (menu == null)
        {
            menu = GameObject.Find("mainMenu");
            
        }
        if (playerSelection == null)
        {
            playerSelection = GameObject.Find("playerSelection");
            
        }
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
    public void changePlayer()
    {        
        // Show player selection (called from UI button)
        if (menu != null) menu.SetActive(false);
        else Debug.LogWarning("`menu` is null when calling changePlayer().", this);

        if (playerSelection != null) playerSelection.SetActive(true);
        else Debug.LogWarning("`playerSelection` is null when calling changePlayer().", this);
    }
    public void backToMenu()
    {
        // Return to main menu (called from UI button)
        if (menu != null) menu.SetActive(true);
        else Debug.LogWarning("`menu` is null when calling backToMenu().", this);

        if (playerSelection != null) playerSelection.SetActive(false);
        else Debug.LogWarning("`playerSelection` is null when calling backToMenu().", this);
    }


}
