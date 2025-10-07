using System.Collections;
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
    public Image shieldBar;
    public bool isHealing = false;
    private float lastDamageTime = -Mathf.Infinity;
    public float healDelay = 5f;
    public float healAmount = 5f;
    private float shield = 0f;
    private bool attackSpeedActive = false;
    private bool speedActive = false;
    private float attackSpeedTimer = 0f;
    private float speedTimer = 0f;
    public float defaultFireRate = 0.5f;
    private float defaultPlayerSpeed 7.5f;

    public float healInterval = 0.5f;
    private float lastHealTime = 0f;
    
    public int maxLives = 3;
    public int currentLives = 3;
    public Image[] lifeImages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerAlive = true;
        UpdateLifeImages();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0f)
        {
            LoseLifeAndRespawn();
        }
        if (Time.time - lastDamageTime > healDelay && currentHealth < maxHealth)
        {
            if (Time.time - lastHealTime > healInterval)
            {
                Heal(healAmount);
                lastHealTime = Time.time;
            }
        }
        if (attackSpeedActive && Time.time >= attackSpeedTimer)
        {
            fireRate = defaultFireRate;
            attackSpeedActive = false;
        }
        if (speedActive && Time.time >= speedTimer)
        {
            playerSpeed = defaultPlayerSpeed;
            speedActive = false;
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
        else if (transform.position.x >= 18)
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
        if (shield > 0)
        {
            float shieldAbsorb = Mathf.Min(shield, damage);
            shield -= shieldAbsorb;
            damage -= shieldAbsorb;
            shieldBar.fillAmount = shield / 100f;
        }

        if (damage > 0)
        {
            currentHealth -= damage;
            healthBar.fillAmount = currentHealth / maxHealth;
            lastDamageTime = Time.time;
        }
    }
    public void Heal(float healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    public void addShield(float shieldAmount)
    {
        shield += shieldAmount;
        shield = Mathf.Clamp(shield, 0, 100f);
        shieldBar.fillAmount = shield / 100f;
    }
    void LoseLifeAndRespawn()
    {
        if (currentLives > 1)
        {
            currentLives--;
            UpdateLifeImages();
            Respawn();
        }
        else
        {
            isPlayerAlive = false;
            Destroy(gameObject);
        }
    }
    void Respawn()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = 1f;
        transform.position = new Vector3(0, -5, 0);
        isPlayerAlive = true;
    }
    void UpdateLifeImages()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = i < currentLives;
        }
    }
    private void OnTriggerEnter2D(Collider2D powerUps)
    {
        if (powerUps.CompareTag("powerup_AS"))
        {
            Destroy(powerUps.gameObject);
            fireRate /= 2;
            Debug.Log("Attack Speed collected!");
            attackSpeedActive = true;
            int powerupDuration = 5;            
        }
        else if (powerUps.CompareTag("powerup_Speed"))
        {
            Destroy(powerUps.gameObject);
            playerSpeed *= 2;
            Debug.Log("Speed collected!");
            speedActive = true;
            int powerupDuration = 5;
        }
        else if (powerUps.name == "powerup_Shield")
        {
            Destroy(powerUps.gameObject);
            addShield(50f);
            Debug.Log("Shield collected!");
            shieldActive = true;
            int powerupDuration = 5;
                    attackSpeedExpireTime = Mathf.Max(attackSpeedExpireTime, Time.time) + powerupDuration;

        }
    }
    IEnumerator powerUpFunc()
    {
        yield return new WaitForSeconds(5);
    }
    
}