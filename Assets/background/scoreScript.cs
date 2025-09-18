using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class scoreScript : MonoBehaviour
{
    
    private int score;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        updateScore(0);
    }

    void Update()
    {
        
    }
    public void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
