using UnityEngine;
using TMPro;
public class playerStats : MonoBehaviour
{    
    TMP_Text shipClassName;
    TMP_Text shipStats;
    public int selectedShipIndex;
    public float playerSpeed;
    public float fireRate;
    public float sDamage;
    public float maxHealth;
    public string className;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateShip();
    }
    public void destroyerClass()
    {
        // Destroyer class stats
        className = "Destroyer";
        playerSpeed = 8f;
        fireRate = 0.2f;
        sDamage = 20f;
        maxHealth = 50f;
        selectedShipIndex = 1;
    }
    public void cruiserClass()
    {     
        // Cruiser class stats
        className = "Cruiser";
        playerSpeed = 7f;
        fireRate = 0.5f;
        sDamage = 20f;
        maxHealth = 100f;
        selectedShipIndex = 0;
    }
    public void battleshipClass()
    {
        // Battleship class stats
        className = "Battleship";
        playerSpeed = 5f;
        fireRate = 1f;
        sDamage = 40f;
        maxHealth = 200f;
        selectedShipIndex = 2;
    }
    public void corvetteClass()
    {
        // Corvette class stats
        className = "Corvette";
        playerSpeed = 10f;
        fireRate = 0.1f;
        sDamage = 10f;
        maxHealth = 30f;
        selectedShipIndex = 3;
    }   
    public void updateShip()
    {
        switch (selectedShipIndex)
        {
            case 0:
                cruiserClass();
                break;
            case 1:
                destroyerClass();
                break;
            case 2:
                battleshipClass();
                break;
            case 3:
                corvetteClass();
                break;
            default:
                cruiserClass();
                break;
        }
    }
    
    

}
