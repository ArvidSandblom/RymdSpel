using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameOver_Score : MonoBehaviour
{

    private int finalScore = 0;
    private string finalUsername = "Player";
    TMP_Text score_GO;
    private shipCarry leaderboard;

    void Start()
    {        
        score_GO = GetComponent<TMP_Text>();
        finalScore = ScoreManager.GetFinalScore();
        
        // Find shipCarry leaderboard
        leaderboard = FindAnyObjectByType<shipCarry>();
        
        // Get username from shipCarry (already captured in menu)
        if (leaderboard != null)
        {
            finalUsername = leaderboard.GetUsername();
            leaderboard.AddScore(finalUsername, finalScore);
        }
        else
        {
            finalUsername = ScoreManager.GetFinalUsername();
        }
    }

    void Update()
    {
        score_GO.text = "Score: " + finalScore;
    }
}
