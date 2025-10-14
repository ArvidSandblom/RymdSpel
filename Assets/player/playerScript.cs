using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    // Movement
    public float playerSpeed = 7.5f;
    // Shooting
    private float fireTimer = 0f;
    public float fireRate = 0.5f;
    public GameObject bullet;
    

    // Health and Shield
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float takeDamage;
    public bool isHealing = false;
    private float lastDamageTime = -Mathf.Infinity;
    public float healDelay = 5f;
    public float healAmount = 5f;
    private float shield = 0f;
    public float healInterval = 0.5f;
    private float lastHealTime = 0f;
    public GameObject shieldObject;
    // UI Elements
    public Image healthBar;
    public Image shieldBar;
    public Image timer_AS;
    public Image timer_Speed;
    // Power-up management
    private bool attackSpeedActive = false;
    private bool speedActive = false;
    private float attackSpeedTimer = 0f;
    private float speedTimer = 0f;
    private float defaultFireRate = 0.5f;
    private float defaultPlayerSpeed = 7.5f;
    private bool shieldActive = false;
    private GameObject activeShieldObject;
    // Lives management
    public static bool isPlayerAlive = true;
    public int maxLives = 3;
    public int currentLives = 3;
    public Image[] lifeImages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerAlive = true;
        UpdateLifeImages();
        defaultFireRate = fireRate;
        defaultPlayerSpeed = playerSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        // Player Death & Healing
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
        // Power-up duration management
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

        // Player Movement
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
        if (Input.GetKey(KeyCode.Space))
        {          
            if (fireTimer <= 0f)
            {
                Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), this.transform.rotation);
                fireTimer = fireRate;
            }            
        }
        // power-up timers
        if (attackSpeedActive)
        {
            float maxASDuration = 5f;
            float remainingAS = Mathf.Clamp(attackSpeedTimer - Time.time, 0f, maxASDuration);
            timer_AS.fillAmount = remainingAS / maxASDuration;
        }
        else
        {
            timer_AS.fillAmount = 0f;
        }
        if (speedActive)
        {
            float maxSpeedDuration = 5f;
            float remainingSpeed = Mathf.Clamp(speedTimer - Time.time, 0f, maxSpeedDuration);
            timer_Speed.fillAmount = remainingSpeed / maxSpeedDuration;
        }
        else
        {
            timer_Speed.fillAmount = 0f;
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
            if (shield <= 0f && shieldActive)
            {
                shield = 0f;
                shieldActive = false;
                if (activeShieldObject != null)
                {
                    Destroy(activeShieldObject);
                    activeShieldObject = null;
                }
            }
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
    //Power-up collection
    private void OnTriggerEnter2D(Collider2D powerUps)
    {
        if (powerUps.CompareTag("powerup_AS"))
        {
            float powerupDuration = 5f;
            Destroy(powerUps.gameObject);            
            if (!attackSpeedActive)
            {
                attackSpeedActive = true;
                fireRate /= 2f;
            }
            attackSpeedTimer = Mathf.Max(attackSpeedTimer, Time.time) + powerupDuration;
        }
        else if (powerUps.CompareTag("powerup_Speed"))
        {
            int powerupDuration = 5;
            Destroy(powerUps.gameObject);                                    
            if (!speedActive)
            {
                speedActive = true;
                playerSpeed *= 2;
            }
            speedTimer = Mathf.Max(speedTimer, Time.time) + powerupDuration;
        }
        else if (powerUps.CompareTag("powerup_Shield"))
        {
            Destroy(powerUps.gameObject);
            addShield(50f);
            if (!shieldActive)
            {
                activeShieldObject = Instantiate(shieldObject, this.transform);
                shieldActive = true;

            }
            

        }        
    }
}