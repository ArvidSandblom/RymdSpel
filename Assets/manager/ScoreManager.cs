using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static int finalScore = 0;
    public static string finalUsername = "Player";
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SaveFinalScore(int score, string username = "Player")
    {
        finalScore = score;
        finalUsername = username;
    }

    public static int GetFinalScore()
    {
        return finalScore;
    }

    public static string GetFinalUsername()
    {
        return finalUsername;
    }
}
