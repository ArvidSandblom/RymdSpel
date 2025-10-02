using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    public float playerSpeed = 7.5f;
    public GameObject bullet;
    public static bool isPlayerAlive = true;

    private float fireTimer = 0f;
    public float fireRate = 0.5f;
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float takeDamage;
    public Image healthBar;
    public bool isHealing = false;
    private float lastDamageTime = -Mathf.Infinity;
    public float healDelay = 5f;
    public float healAmount = 5f;
    public float healInterval = 0.5f;
    private float lastHealTime = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0f)
        {
            isPlayerAlive = false;
            Destroy(gameObject);

        }
        if (Time.time - lastDamageTime > healDelay && currentHealth < maxHealth)
        {
            if (Time.time - lastHealTime > healInterval)
            {
                Heal(healAmount);
                lastHealTime = Time.time;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y <= 0)
            {
                transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);
            }
            else if (transform.position.y >= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
            }
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y >= -9f)
            {
                transform.Translate(Vector3.down * playerSpeed * Time.deltaTime);
            }
            else if (transform.position.y <= -9)
            {
                transform.position = new Vector3(transform.position.x, -9f, 0);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        if (transform.position.x <= -18)
        {
            transform.position = new Vector3(18f, transform.position.y, 0);
        }
        if (transform.position.x >= 18)
        {
            transform.position = new Vector3(-18f, transform.position.y, 0);
        }
        fireTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), this.transform.rotation);
            fireTimer = fireRate;
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
        lastDamageTime = Time.time;

    }
    public void Heal(float healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.fillAmount = currentHealth / maxHealth;        
    }

}