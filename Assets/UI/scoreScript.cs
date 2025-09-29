using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreScript : MonoBehaviour
{

    public static int scoreValue = 0;
    TMP_Text score;
    void Start()
    {
        scoreValue = 0;
        score = GetComponent<TMP_Text>();
    }

    void Update()
    {
        score.text = "Score: " + scoreValue;
    }
}
