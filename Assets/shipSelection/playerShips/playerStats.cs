using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class playerStats : MonoBehaviour
{    
    TMP_Text shipClassName;
    TMP_Text shipStats;
    public Image shipImage;
    public Sprite[] shipSprites;
    public int selectedShipIndex;
    public float playerSpeed;
    public float fireRate;
    public float sDamage;
    public float maxHealth;
    public string className;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.Find("statsManager") && GameObject.Find("statsManager") != this.gameObject)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            mainMenuStatsDisplay();
        }        
        updateShip();
    }
    void mainMenuStatsDisplay()
    {

        if (shipClassName == null)
        {
            var go = GameObject.Find("shipName");
            if (go != null) shipClassName = go.GetComponent<TMP_Text>();
            else Debug.LogWarning("Could not find GameObject 'shipName' for UI text.", this);
        }

        if (shipStats == null)
        {
            var go = GameObject.Find("shipStatistics");
            if (go != null) shipStats = go.GetComponent<TMP_Text>();
            else Debug.LogWarning("Could not find GameObject 'shipStatistics' for UI text.", this);
        }

        if (shipImage == null)
        {
            var go = GameObject.Find("Image");
            if (go != null) shipImage = go.GetComponent<Image>();
            else Debug.LogWarning("Could not find GameObject 'Image' for ship preview.", this);
        }

        if (shipImage != null && shipSprites != null && shipSprites.Length > 0 && selectedShipIndex >= 0 && selectedShipIndex < shipSprites.Length)
        {
            shipImage.sprite = shipSprites[selectedShipIndex];
        }

        if (shipClassName != null)
        {
            shipClassName.text = className + " Class";
        }

        if (shipStats != null)
        {
            shipStats.text = "Speed: " + playerSpeed.ToString() + "\n" +
                             "Fire Rate: " + fireRate.ToString() + "\n" +
                             "Damage: " + sDamage.ToString() + "\n" +
                             "Max Health: " + maxHealth.ToString();
        }
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
