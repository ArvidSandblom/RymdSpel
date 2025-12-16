using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class managerScript : MonoBehaviour
{    
    private GameObject scoreText;
    public GameObject powerUp_Shield;
    public GameObject powerUp_Speed;
    public GameObject powerUp_AS;
    public float spawnInterval = 10f;
    public GameObject enemy;
    public GameObject boss;
    public static int enemyCounter = 0;
    public static int enemiesDestroyed = 0;
    private float spawnTimer;
    private bool spawnEnemies;
    private float powerupSpawnTimer;
    private float enemySpawnTimer;
    private bool bossSpawned = false;
    private int level = 1;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level *= 3;
        for (int i = 0; i < level; i++)
        {
            spawnInterval = Mathf.Max(1f, spawnInterval - 1f);
            Instantiate(enemy, new Vector3(Random.Range(-19f, 19f), 11f, 0f), Quaternion.identity);
        }

        scoreText = GameObject.Find("score");

        // initialize timers
        powerupSpawnTimer = Random.Range(3f, 5f);
        enemySpawnTimer = spawnInterval;

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
            if (enemiesDestroyed >= 10 && !bossSpawned)
            {
                Instantiate(boss, new Vector3(0f, 11f, 0f), Quaternion.identity);
                foreach (var e in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(e);
                }
                for (int i = -level; i < level; i++) //spawn a wave of enemies when boss is instantied on each side
                {
                    Instantiate(enemy, new Vector3(i * 2f, 11f, 0f), Quaternion.identity);
                    enemyCounter++;
                }
                bossSpawned = true;
                yield return null;
            }
            else if (enemyCounter <= 5 && GameObject.Find("player") != null && enemiesDestroyed < 10)
            {
                Instantiate(enemy, new Vector3(Random.Range(-19f, 19f), 11f, 0f), Quaternion.identity);
                enemyCounter++;
                enemySpawnTimer = Random.Range(1f, Mathf.Max(1f, spawnInterval));
                yield return new WaitForSeconds(enemySpawnTimer);
            }
            else yield return null;
        }
    }
}
