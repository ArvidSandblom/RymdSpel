using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public static int enemyCounter = 0;
    private int spawnTimer = 5;

    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    void Update()
    {

    }

    IEnumerator EnemySpawner()
    {
        while (playerScript.isPlayerAlive)
        {
            if (enemyCounter <= 3 && GameObject.Find("player") != null)
            {
                Instantiate(enemy, new Vector3(Random.Range(-19,19), 11, 0), Quaternion.identity);
                enemyCounter++;
                yield return new WaitForSeconds(spawnTimer);
            }
            else yield return null;
        }
    }
}