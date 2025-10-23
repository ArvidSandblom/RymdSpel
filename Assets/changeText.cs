using UnityEngine;
using TMPro;

public class changeText : MonoBehaviour
{
    public GameObject playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.Find("statsManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void textChange()
    {

    }
}
