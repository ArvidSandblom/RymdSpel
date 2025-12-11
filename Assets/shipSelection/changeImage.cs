using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeImage : MonoBehaviour
{
    public Image ship;
    public Sprite[] ships;
    public int currentIndex;
    public GameObject playerStats;
    public TMP_Text shipStats;
    public TMP_Text shipClassName;
    public string className;
    public string stats;
    private GameObject plusButton;
    private GameObject minusButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plusButton = GameObject.Find("imageScroll +1");
        minusButton = GameObject.Find("imageScroll -1");
        playerStats = GameObject.Find("statsManager");
        currentIndex = 0;
        minusButton.SetActive(false);
        imageScroll(currentIndex);
        playerStats.GetComponent<playerStats>().selectedShipIndex = currentIndex;
        stats = "Speed: " + playerStats.GetComponent<playerStats>().playerSpeed + "\n" +
        "Reload Time: " + playerStats.GetComponent<playerStats>().fireRate + "s\n" +
        "Damage: " + playerStats.GetComponent<playerStats>().sDamage + "\n" +
        "Max Health: " + playerStats.GetComponent<playerStats>().maxHealth;
        className = playerStats.GetComponent<playerStats>().className + " Class";

    }

    // Update is called once per frame
    void Update()
    {
        var ps = playerStats.GetComponent<playerStats>();
        if (ps != null)
        {
            stats = "Speed: " + ps.playerSpeed + "\n" +
                    "Reload Time: " + ps.fireRate + "s\n" +
                    "Damage: " + ps.sDamage + "\n" +
                    "Max Health: " + ps.maxHealth;
            className = ps.className + " Class";
        }
        shipStats.text = stats;
        shipClassName.text = className;
    }

    void imageScroll(int spriteIndex)
    {
        ship.sprite = ships[spriteIndex];
    }
    public void selectMinus()
    {
        if (currentIndex <= 0)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex--;
            imageScroll(currentIndex);
        }
        playerStats.GetComponent<playerStats>().selectedShipIndex = currentIndex;
        stats = "Speed: " + playerStats.GetComponent<playerStats>().playerSpeed + "\n" +
        "Reload Time: " + playerStats.GetComponent<playerStats>().fireRate + "/s\n" +
        "Damage: " + playerStats.GetComponent<playerStats>().sDamage + "\n" +
        "Max Health: " + playerStats.GetComponent<playerStats>().maxHealth;
        className = playerStats.GetComponent<playerStats>().className + " Class";

        if (currentIndex == 0)
        {
            minusButton.SetActive(false);
        }
        if (currentIndex < ships.Length - 1)
        {
            plusButton.SetActive(true);
        }
        
    }
    public void selectPlus()
    {
        if (currentIndex >= ships.Length - 1)
        {
            currentIndex = ships.Length - 1;
        }
        else
        {
            currentIndex++;
            imageScroll(currentIndex);
        }
        
        playerStats.GetComponent<playerStats>().selectedShipIndex = currentIndex;
        stats = "Speed: " + playerStats.GetComponent<playerStats>().playerSpeed + "\n" +
        "Reload Time: " + playerStats.GetComponent<playerStats>().fireRate + "s\n" +
        "Damage: " + playerStats.GetComponent<playerStats>().sDamage + "\n" +
        "Max Health: " + playerStats.GetComponent<playerStats>().maxHealth;
        className = playerStats.GetComponent<playerStats>().className + " Class";

        if (currentIndex == ships.Length - 1)
        {
            plusButton.SetActive(false);
        }
        if (currentIndex > 0)
        {
            minusButton.SetActive(true);
        }
    }    
}
