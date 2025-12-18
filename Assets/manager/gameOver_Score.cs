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
    }

    void Update()
    {
        score_GO.text = "Score: " + finalScore;
    }
}
