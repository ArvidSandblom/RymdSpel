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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GameObject.Find("score");
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
            spawnTimer = Random.Range(3f, 5f);
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
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator EnemySpawner()
    {
        while (playerScript.isPlayerAlive)
        {
            if (enemiesDestroyed == 10)
            {
                Instantiate(boss, new Vector3(0, 11, 0), Quaternion.identity);
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(enemy);
                }                
                yield break;
            }
            else if (enemyCounter <= 5 && GameObject.Find("player") != null && enemiesDestroyed < 10)
            {
                Instantiate(enemy, new Vector3(Random.Range(-19, 19), 11, 0), Quaternion.identity);
                enemyCounter++;
                yield return new WaitForSeconds(spawnTimer);
            }
            else yield return null;
        }
    }
}
