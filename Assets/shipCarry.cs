using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class shipCarry : MonoBehaviour
{
    public int[] scoreValues = new int[10];
    public string[] playerNames = new string[10];
    public TMP_Text highScoreText;
    public InputField usernameInputField;
    private int scoreCount = 0;
    public string userName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        string displayText = "";
        for (int i = 0; i < scoreCount; i++)
        {
            displayText += "Player: " + playerNames[i] + " Score: " + scoreValues[i] + "\n";
        }
        if (highScoreText != null)
        {
            highScoreText.text = displayText;
        }
    }

    public void AddScore(string playerName, int score)
    {
        if (scoreCount < scoreValues.Length)
        {
            playerNames[scoreCount] = playerName;
            scoreValues[scoreCount] = score;
            scoreCount++;
            SortScores();
        }
    }

    private void SortScores()
    {
        // Bubble sort scores in descending order
        for (int i = 0; i < scoreCount - 1; i++)
        {
            for (int j = 0; j < scoreCount - i - 1; j++)
            {
                if (scoreValues[j] < scoreValues[j + 1])
                {
                    // Swap scores
                    int tempScore = scoreValues[j];
                    scoreValues[j] = scoreValues[j + 1];
                    scoreValues[j + 1] = tempScore;

                    // Swap names
                    string tempName = playerNames[j];
                    playerNames[j] = playerNames[j + 1];
                    playerNames[j + 1] = tempName;
                }
            }
        }
    }

    public void SetUsernameFromInput(string text)
    {
        if (usernameInputField != null)
        {
            userName = text;
            if (string.IsNullOrEmpty(userName))
            {
                userName = "Player";
            }
        }
        else
        {
            Debug.LogWarning("UsernameInputField is not assigned!");
        }
    }

    public string GetUsername()
    {
        return userName;
    }
    
}
