using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class highScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;
    public int scoreValueCopy;
    TMP_Text score;
    void Start()
    {
        scoreValue = 0;
        score = GetComponent<TMP_Text>();
    }

    void Update()
    {
        scoreValueCopy = scoreValue;
        score.text = "Score: " + scoreValue;
    }
}
