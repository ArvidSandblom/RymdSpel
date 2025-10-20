using UnityEngine;

public class playerStats : MonoBehaviour
{
    public string stats = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void destroyerClass()
    {
        // Destroyer class stats

        float playerSpeed = 8f;
        float fireRate = 0.2f;
        float damage = 20f;
        float maxHealth = 50f;
    }
    public void cruiserClass()
    {     
        // Cruiser class stats
        float playerSpeed = 7f;
        float fireRate = 0.5f;
        float damage = 10f;
        float maxHealth = 100f;
    }


}
