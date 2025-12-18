using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class playerScript : MonoBehaviour
{
    private float leanAngle = 40f;
    private float leanSpeed = 10f;
    // Movement
    public float playerSpeed = 7f;
    public float moveSpeedMultiplier = 1f;
    public float dodgeChance = 0f;
    // Shooting
    private float nextFireTime = 0f;
    public bool sideShots = false;
    public bool homingShots = false;
    public bool hasEMP = false;
    public float fireRate = 0.5f;
    public float attackSpeedMultiplier = 1f;
    public GameObject bullet;
    public GameObject homingBullet;
    public float extraBulletChance = 0;
    public float damage = 20f;
    public Sprite[] ships;
    public GameObject playerStats;
    public GameObject friend;
    public GameObject pauseMenu;
    public int sideShotsIndexMinus = 0;
    public int sideShotsIndexPlus = 0;

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
    public float shieldMultiplier = 1f;
    // Lives management
    public static bool isPlayerAlive = true;
    public int maxLives = 3;
    public int currentLives = 3;
    public Image[] lifeImages;
    public Image experienceBar;
    public static float experiencePoints = 0f;
    public static float experienceToNextLevel = 250f;
    public float experienceMultiplier = 1f;
    public static int currentLevel = 1;
    public TMP_Text level;
    public GameObject levelUpButton;
    public GameObject levelUpScreen;
    public GameObject manager;
    public float EMPCooldown = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevel = 1;
        experienceToNextLevel = 250f;
        manager = GameObject.Find("manager");
        shieldObject = GameObject.Find("shield");
        playerStats = GameObject.Find("statsManager");
        isPlayerAlive = true;
        UpdateLifeImages();
        UpdatePlayerStats();
        defaultFireRate = fireRate;
        defaultPlayerSpeed = playerSpeed;
        shieldObject.SetActive(false);
        //levelUpButton.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
            else{
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
        }
        // Player Movement
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
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
        if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow))
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
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
        
        // tilt längs Y axeln, kanske tar bort
        float horizontal = Input.GetAxis("Horizontal");
        float targetY = -horizontal * leanAngle;
        Quaternion targetRotation = Quaternion.Euler(0, targetY, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * leanSpeed);

        EMPCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && hasEMP && EMPCooldown <= 0f)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies){
                enemy.GetComponent<enemyLogic>().isEMP = true;
            }
            EMPCooldown = 20f;
        }

        if (!isPlayerAlive) return;

        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.time >= nextFireTime)
            {
                
                try
                {
                if (extraBulletChance >= Random.Range(0, 101))
                {
                    for (int j = 1; j <= 2; j++)
                    {

                    
                    if (homingShots && sideShots)
                    {
                        for (int i = sideShotsIndexMinus; i <= sideShotsIndexPlus; i++)
                        {
                            GameObject thisBullet = Instantiate(homingBullet, new Vector3(this.transform.position.x, this.transform.position.y + (0.5f * j), 0), Quaternion.Euler(0, 0, Mathf.Atan2(0, 0) * Mathf.Rad2Deg + (i * 15f)));
                            if (thisBullet != null)
                            {
                                if (thisBullet.TryGetComponent<bulletLogic>(out var bl)) bl.bulletDamage = damage;
                                else if (thisBullet.TryGetComponent<homingBullet>(out var hb)) hb.bulletDamage = damage;
                            }
                        }
                        nextFireTime = Time.time + fireRate;
                    }
                    else if (homingShots)
                    {
                        GameObject thisBullet = Instantiate(homingBullet, new Vector3(this.transform.position.x, this.transform.position.y + (0.5f * j), 0), this.transform.rotation);
                        if (thisBullet != null)
                        {
                            if (thisBullet.TryGetComponent<bulletLogic>(out var bl)) bl.bulletDamage = damage;
                            else if (thisBullet.TryGetComponent<homingBullet>(out var hb)) hb.bulletDamage = damage;
                        }
                        nextFireTime = Time.time + fireRate;

                    }
                    else if (sideShots)
                    {
                        for (int i = sideShotsIndexMinus; i <= sideShotsIndexPlus; i++)
                        {
                            GameObject thisBullet = Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + (0.5f * j), 0), Quaternion.Euler(0, 0, Mathf.Atan2(0, 0) * Mathf.Rad2Deg + (i * 15f)));
                            thisBullet.GetComponent<bulletLogic>().bulletDamage = damage;
                        }
                        nextFireTime = Time.time + fireRate;
                    }
                    else
                    {
                        GameObject thisBullet = Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + (0.5f * j), 0), this.transform.rotation);
                        thisBullet.GetComponent<bulletLogic>().bulletDamage = damage;
                        nextFireTime = Time.time + fireRate;

                    }
                    }
                }
                else
                    {
                        if (homingShots && sideShots)
                    {
                        for (int i = sideShotsIndexMinus; i <= sideShotsIndexPlus; i++)
                        {
                            GameObject thisBullet = Instantiate(homingBullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), Quaternion.Euler(0, 0, Mathf.Atan2(0, 0) * Mathf.Rad2Deg + (i * 15f)));
                            if (thisBullet != null)
                            {
                                if (thisBullet.TryGetComponent<bulletLogic>(out var bl)) bl.bulletDamage = damage;
                                else if (thisBullet.TryGetComponent<homingBullet>(out var hb)) hb.bulletDamage = damage;
                            }
                        }
                        nextFireTime = Time.time + fireRate;
                    }
                    else if (homingShots)
                    {
                        GameObject thisBullet = Instantiate(homingBullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), this.transform.rotation);
                        if (thisBullet != null)
                        {
                            if (thisBullet.TryGetComponent<bulletLogic>(out var bl)) bl.bulletDamage = damage;
                            else if (thisBullet.TryGetComponent<homingBullet>(out var hb)) hb.bulletDamage = damage;
                        }
                        nextFireTime = Time.time + fireRate;

                    }
                    else if (sideShots)
                    {
                        for (int i = sideShotsIndexMinus; i <= sideShotsIndexPlus; i++)
                        {
                            GameObject thisBullet = Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), Quaternion.Euler(0, 0, Mathf.Atan2(0, 0) * Mathf.Rad2Deg + (i * 15f)));
                            thisBullet.GetComponent<bulletLogic>().bulletDamage = damage;
                        }
                        nextFireTime = Time.time + fireRate;
                    }
                    else
                    {
                        GameObject thisBullet = Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), this.transform.rotation);
                        thisBullet.GetComponent<bulletLogic>().bulletDamage = damage;
                        nextFireTime = Time.time + fireRate;

                    }
                    }
                    
                }
                
                catch (System.Exception e)
                {
                    Debug.LogException(e, this);
                }

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
        if (shield > 0 && shieldActive)
        {
            float shieldAbsorb = Mathf.Min(shield, damage);
            shield -= shieldAbsorb;
            damage -= shieldAbsorb;
            shieldBar.fillAmount = shield / maxHealth;
        }
        if (shield <= 0 && shieldActive)
        {
            shieldActive = false;
            shieldObject.SetActive(false);
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
        shield = Mathf.Clamp(shield, 0, maxHealth);
        shieldBar.fillAmount = shield / maxHealth;
    }
    void LoseLifeAndRespawn()
    {
        if (currentLives > 1)
        {
            currentLives--;
            if (shieldActive)
            {
                shieldActive = false;
                shieldObject.SetActive(false);
            }
            UpdateLifeImages();
            Respawn();
        }
        else
        {
            isPlayerAlive = false;
            ScoreManager.SaveFinalScore(highScoreScript.scoreValue, "Player");
            Destroy(gameObject);
            SceneManager.LoadScene(2);
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
                fireRate /= 2f * attackSpeedMultiplier;
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
            addShield(50f * shieldMultiplier);
            if (!shieldActive)
            {                
                shieldObject.SetActive(true);
                shieldActive = true;
            }
            

        }        
    }
    public void addExperience(float experience)
    {
        // Add XP to the stored points and update the progress bar.
        experiencePoints += experience * experienceMultiplier;
        // If we've reached or exceeded the threshold, allow level-up and keep remainder
        if (experiencePoints >= experienceToNextLevel)
        {
            levelUpButton.SetActive(true);
        }
        // Update progress UI (clamped 0..1)
        experienceBar.fillAmount = Mathf.Clamp01(experiencePoints / experienceToNextLevel);

    }
    public void levelUp()
    {
        experiencePoints -= experienceToNextLevel;
        // Consume level-up and prepare for next level
        levelUpButton.SetActive(false);
        currentLevel += 1;
        level.text = ""+currentLevel;
        experienceToNextLevel *= 1.3f;
        addExperience(0);
        // Ensure progress bar reflects leftover XP toward new threshold
        experienceBar.fillAmount = Mathf.Clamp01(experiencePoints / experienceToNextLevel);
        levelUpScreen.SetActive(true);
        manager.GetComponent<managerScript>().levelUp();
    }
    //public void levelUp
    public void UpdatePlayerStats()
    {
        int selectedIndex = playerStats.GetComponent<playerStats>().selectedShipIndex;
        switch (selectedIndex)
        {
            case 0:
                playerStats.GetComponent<playerStats>().cruiserClass();
                break;
            case 1:
                playerStats.GetComponent<playerStats>().destroyerClass();
                break;
            case 2:
                playerStats.GetComponent<playerStats>().battleshipClass();
                break;
            case 3:
                playerStats.GetComponent<playerStats>().corvetteClass();
                break;
            default:
                playerStats.GetComponent<playerStats>().cruiserClass();
                break;
        }
        // Apply stats to player
        playerSpeed = playerStats.GetComponent<playerStats>().playerSpeed;
        fireRate = playerStats.GetComponent<playerStats>().fireRate;
        damage = playerStats.GetComponent<playerStats>().sDamage;
        maxHealth = playerStats.GetComponent<playerStats>().maxHealth;
        currentHealth = maxHealth;
        healthBar.fillAmount = 1f;
        // Update ship sprite
        GetComponent<SpriteRenderer>().sprite = ships[selectedIndex];
    }
}