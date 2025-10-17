using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameOver_Score : MonoBehaviour
{

    public static int scoreValue = scoreScript.scoreValue;
    TMP_Text score_GO;

    void Start()
    {        
        score_GO = GetComponent<TMP_Text>();
    }

    void Update()
    {
        score_GO.text = "Score: " + scoreValue;
    }
}
