using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class managerScript : MonoBehaviour
{    
    private GameObject scoreText;
    public GameObject powerUp_Shield;
    public GameObject powerUp_Speed;
    public GameObject powerUp_AS;
    public float spawnInterval = 10f;
    public GameObject enemy;
    public GameObject boss;
    public GameObject player;
    public static int enemyCounter = 0;
    public static int enemiesDestroyed = 0;
    private float powerupSpawnTimer;
    public static float enemySpawnTimer;
    private int level = 1;
    public static int bossesDestroyed = 0;
    public TMP_Text bossText;
    public GameObject pauseMenu;
    public GameObject friend;
    public GameObject levelUpScreen;
    public GameObject[] levelUpScreensCommon;
    public GameObject[] levelUpScreensUncommon;
    public GameObject[] levelUpScreensRare;
    public GameObject[] levelUpScreensEpic;
    public GameObject levelUpScreenLegendary;
    public Transform[] levelUpPositions; // assign 3 UI positions in Inspector
    public Transform levelUpPoolParent; // optional: assign a transform to keep pooled option objects
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCounter = 0;
        enemiesDestroyed = 0;
        bossesDestroyed = 0;
        player = GameObject.FindWithTag("Player");
        level *= 3;

        scoreText = GameObject.Find("score");

        // initialize timers
        powerupSpawnTimer = Random.Range(3f, 5f);
        enemySpawnTimer = spawnInterval;
        for (int i = 0; i < level; i++)
        {
            spawnInterval = Mathf.Max(1f, spawnInterval - 1f);
            GameObject thisEnemy = Instantiate(enemy, new Vector3(Random.Range(-19f, 19f), 11f, 0f), Quaternion.identity);
            // scale health and bullet damage based on bosses destroyed (use float division)
            var el = thisEnemy.GetComponent<enemyLogic>();
            if (el != null)
            {
                float scale = 1f + bossesDestroyed / 10f;
                el.health *= scale;
                if (el.enemyBullet != null)
                {
                    var eb = el.enemyBullet.GetComponent<enemyBullet>();
                    if (eb != null) eb.damage *= (1f + bossesDestroyed / 20f);
                }
            }
            enemyCounter++;
        }

        StartCoroutine(SpawnPowerup());
        StartCoroutine(EnemySpawner());
        //Instantiate(powerUp_BigLaser, new Vector3(3, -4, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnPowerup()
    {
        
        while (playerScript.isPlayerAlive)
        {
            powerupSpawnTimer = Random.Range(3f, 5f);
            float randomX = Random.Range(-12f, 12f);
            float randomY = Random.Range(-6f, -0.5f);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
            int randomPowerup = Random.Range(0, 6);
            if (randomPowerup == 0 || randomPowerup == 1)
            {
                Instantiate(powerUp_Shield, spawnPosition, Quaternion.identity);
            }
            else if (randomPowerup == 2 || randomPowerup == 3)
            {
                Instantiate(powerUp_Speed, spawnPosition, Quaternion.identity);
            }
            else if (randomPowerup == 4 || randomPowerup == 5)
            {
                Instantiate(powerUp_AS, spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(powerupSpawnTimer);
        }
    }
    IEnumerator EnemySpawner()
    {
        while (playerScript.isPlayerAlive)
        {
            // If there are no enemies on the map, spawn 3 and speed up spawn rate
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                for (int s = 0; s < 3; s++)
                {
                    GameObject thisEnemy = Instantiate(this.enemy, new Vector3(Random.Range(-12f, 12f), 11f, 0f), Quaternion.identity);
                    var el = thisEnemy.GetComponent<enemyLogic>();
                    if (el != null)
                    {
                        float scale = 1f + bossesDestroyed / 10f;
                        el.health *= scale;
                        if (el.enemyBullet != null)
                        {
                            var eb = el.enemyBullet.GetComponent<enemyBullet>();
                            if (eb != null) eb.damage *= (1f + bossesDestroyed / 20f);
                        }
                    }
                    enemyCounter++;
                }
                // Halve spawn interval (spawn more frequently), with a reasonable minimum
                spawnInterval = Mathf.Max(0.5f, spawnInterval / 2f);
                enemySpawnTimer = spawnInterval;
                // Give one frame to let spawned enemies register
                yield return null;
            }
            if (enemiesDestroyed >= 10 + bossesDestroyed)
            {
                Instantiate(boss, new Vector3(0f, 11f, 0f), Quaternion.identity);                
                foreach (var e in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(e);
                    enemyCounter--;
                }
                for (int i = -level; i < level; i++) //spawn a wave of enemies when boss is instantiated on each side
                {
                    GameObject thisEnemy = Instantiate(this.enemy, new Vector3(i * 2f, 11f, 0f), Quaternion.identity);
                    var el = thisEnemy.GetComponent<enemyLogic>();
                    if (el != null)
                    {
                        float scale = 1f + bossesDestroyed / 10f;
                        el.health *= scale;
                        if (el.enemyBullet != null)
                        {
                            var eb = el.enemyBullet.GetComponent<enemyBullet>();
                            if (eb != null) eb.damage *= (1f + bossesDestroyed / 20f);
                        }
                    }
                    enemyCounter++;
                }
                StartCoroutine(bossApproach(bossText));
                enemiesDestroyed = 0;
                yield return null;
            }
            else if (enemyCounter <= 5 + bossesDestroyed && GameObject.FindGameObjectWithTag("Player") != null)
            {
                GameObject thisEnemy = Instantiate(this.enemy, new Vector3(Random.Range(-19f, 19f), 11f, 0f), Quaternion.identity);
                var el = thisEnemy.GetComponent<enemyLogic>();
                if (el != null)
                {
                    float scale = 1f + bossesDestroyed / 10f;
                    el.health *= scale;
                    if (el.enemyBullet != null)
                    {
                        var eb = el.enemyBullet.GetComponent<enemyBullet>();
                        if (eb != null) eb.damage *= (1f + bossesDestroyed / 20f);
                    }
                }
                enemyCounter++;
                enemySpawnTimer = Random.Range(1f, Mathf.Max(1f, spawnInterval));
                float coolDownReduction;
                if (bossesDestroyed == 0)
                {
                    coolDownReduction = 1;
                } 
                else coolDownReduction = 1+bossesDestroyed / 10;
                
                yield return new WaitForSeconds(enemySpawnTimer/coolDownReduction);
            }
            else yield return null;
        }
    }
    IEnumerator bossApproach(TMP_Text textElement)
    {
        //activate text element
        textElement.gameObject.SetActive(true);
                
        float fadeDuration = 0.5f;
        float displayDuration = 1f;

        //Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = t / fadeDuration;
            textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, alpha);
            yield return null;
        }
        textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, 1f);

        yield return new WaitForSeconds(displayDuration);

        //Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = 1f - (t / fadeDuration);
            textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, alpha);
            yield return null;
        }
        textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, 0f);

        //deactivate text element
        textElement.gameObject.SetActive(false);
    }
    public void levelUp()
    {
        // Move any existing option objects back to the pool (don't destroy so button callbacks remain intact)
        if (levelUpPoolParent == null)
        {
            // create a hidden pool under this manager
            GameObject poolGo = GameObject.Find("LevelUpPool");
            if (poolGo == null)
            {
                poolGo = new GameObject("LevelUpPool");
                poolGo.transform.SetParent(this.transform, false);
                poolGo.SetActive(false);
            }
            levelUpPoolParent = poolGo.transform;
        }

        for (int c = levelUpScreen.transform.childCount - 1; c >= 0; c--)
        {
            var child = levelUpScreen.transform.GetChild(c).gameObject;
            child.transform.SetParent(levelUpPoolParent, false);
            child.SetActive(false);
        }

        if (levelUpPositions == null || levelUpPositions.Length < 3)
        {
            Debug.LogWarning("Please assign 3 `levelUpPositions` in managerScript to place rolled options.", this);
        }

        List<GameObject> chosen = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            int roll = Random.Range(0, 101); //Common 50%, Uncommon 25%, Rare 15%, Epic 8%, Legendary 2%
            GameObject[] source = levelUpScreensCommon;

            if (roll <= 49) source = levelUpScreensCommon;
            else if (roll <= 74) source = levelUpScreensUncommon;
            else if (roll <= 89) source = levelUpScreensRare;
            else if (roll <= 97) source = levelUpScreensEpic;
            else source = new GameObject[] { levelUpScreenLegendary };

            if (source == null || source.Length == 0)
            {
                Debug.LogWarning("No level-up prefabs available for selected rarity.", this);
                continue;
            }

            // pick a prefab, avoid duplicates when possible
            GameObject prefab = null;
            int attempts = 0;
            while (attempts < 10)
            {
                var candidate = source[Random.Range(0, source.Length)];
                if (!chosen.Contains(candidate))
                {
                    prefab = candidate;
                    break;
                }
                attempts++;
            }
            if (prefab == null) prefab = source[Random.Range(0, source.Length)];

            chosen.Add(prefab);

            // Try to reuse a pooled instance of this prefab (match by prefab name)
            GameObject go = null;
            if (levelUpPoolParent != null)
            {
                for (int p = 0; p < levelUpPoolParent.childCount; p++)
                {
                    var pooled = levelUpPoolParent.GetChild(p).gameObject;
                    if (pooled != null && pooled.name.Contains(prefab.name))
                    {
                        go = pooled;
                        break;
                    }
                }
            }

            if (go != null)
            {
                go.transform.SetParent(levelUpScreen.transform, false);
                go.SetActive(true);
            }
            else
            {
                go = Instantiate(prefab, levelUpScreen.transform);
            }
            // If RectTransforms are present, use anchoredPosition to place UI correctly
            RectTransform goRect = go.GetComponent<RectTransform>();
            RectTransform posRect = (levelUpPositions != null && i < levelUpPositions.Length) ? levelUpPositions[i].GetComponent<RectTransform>() : null;
            if (goRect != null && posRect != null)
            {
                goRect.anchoredPosition = posRect.anchoredPosition;
                goRect.localRotation = posRect.localRotation;
            }
            else if (levelUpPositions != null && i < levelUpPositions.Length && levelUpPositions[i] != null)
            {
                go.transform.position = levelUpPositions[i].position;
                go.transform.rotation = levelUpPositions[i].rotation;
            }
        }
        levelUpScreen.SetActive(true);
        Time.timeScale = 0;
    }    
    
    public void levelUpHealth() // Common
    {
        player.GetComponent<playerScript>().maxHealth *= 1.1f;
        stopPauseAndContinue();
    }
    public void levelUpAttackDamage() // Common
    {
        player.GetComponent<playerScript>().damage *= 1.1f;
        stopPauseAndContinue();
    }
    public void levelUpAttackSpeed() // Common
    {
        player.GetComponent<playerScript>().fireRate *= 0.9f;
        stopPauseAndContinue();
    }
    public void levelUpMoveSpeedUp() // Common
    {
        player.GetComponent<playerScript>().playerSpeed *= 1.1f;
        stopPauseAndContinue();
    }
    public void levelUpExtraShotChance() // Common
    {
        player.GetComponent<playerScript>().extraBulletChance += 10f;
        stopPauseAndContinue();
    }
    public void levelUpShieldHP() // Uncommon Shield powerup amount increase
    {
        player.GetComponent<playerScript>().shieldMultiplier += 0.1f;
        stopPauseAndContinue();
    }
    public void levelUpAttackSpeedBoostAmount() // Uncommon Attackspeed powerup amount increase
    {
        player.GetComponent<playerScript>().attackSpeedMultiplier += 0.1f;
        stopPauseAndContinue();
    }
    public void levelUpMovementSpeedBoostAmount() // Uncommon Movement powerup speed amount increase 
    {
        player.GetComponent<playerScript>().moveSpeedMultiplier += 0.1f;
        stopPauseAndContinue();
    }
    public void levelUpDodgeChance() // Uncommon
    {        
        player.GetComponent<playerScript>().dodgeChance += 0.05f;    
        stopPauseAndContinue();    
    }
    public void levelUpExperienceGain() // Rare
    {
        player.GetComponent<playerScript>().experienceMultiplier += 0.1f;
        stopPauseAndContinue();
    }
    public void levelUpAddFriend() // Rare
    {
        GameObject thisFriend;
        thisFriend = Instantiate(friend, new Vector3(player.transform.position.x, -7.5f, 0), player.transform.rotation);
        stopPauseAndContinue();
    }
    public void levelUpMissileShot() // Rare
    {

    }
    public void levelUpSideShots() // Epic
    {
        if (!player.GetComponent<playerScript>().sideShots){

            player.GetComponent<playerScript>().sideShots = true;
        }
        else {
            player.GetComponent<playerScript>().sideShotsIndexMinus--;
            player.GetComponent<playerScript>().sideShotsIndexPlus++;
            
        }
        stopPauseAndContinue();
    }
    public void levelUpEMP() // Epic
    {
        if (!player.GetComponent<playerScript>().hasEMP){

            player.GetComponent<playerScript>().hasEMP = true;
        }
        else {
            player.GetComponent<playerScript>().EMPCooldown/=1.2f;
        }
        stopPauseAndContinue();
    }
    public void levelUpHomingShots() // Legendary
    {
        if (!player.GetComponent<playerScript>().homingShots){

        player.GetComponent<playerScript>().homingShots = true;
        }
        else {
            player.GetComponent<homingBullet>().turnSpeed+=10f;
        }
        stopPauseAndContinue();
    }
    
    // Ensure pool parent exists
    private void EnsureLevelUpPool()
    {
        if (levelUpPoolParent == null)
        {
            GameObject poolGo = GameObject.Find("LevelUpPool");
            if (poolGo == null)
            {
                poolGo = new GameObject("LevelUpPool");
                poolGo.transform.SetParent(this.transform, false);
                poolGo.SetActive(false);
            }
            levelUpPoolParent = poolGo.transform;
        }
    }
    
    // Return any instantiated level-up option objects back to the pool and hide the levelUpScreen
    public void stopPauseAndContinue()
    {
        EnsureLevelUpPool();
        if (levelUpScreen != null)
        {
            for (int c = levelUpScreen.transform.childCount - 1; c >= 0; c--)
            {
                var child = levelUpScreen.transform.GetChild(c).gameObject;
                child.transform.SetParent(levelUpPoolParent, false);
                child.SetActive(false);
            }
            levelUpScreen.SetActive(false);
        }
        Time.timeScale = 1f;
    }
    public void backToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

    }

}
