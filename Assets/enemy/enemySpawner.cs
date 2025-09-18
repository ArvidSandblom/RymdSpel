using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    public GameObject enemy;
    private int enemyCounter;
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
        while (true)
        {
            if (enemyCounter < 3 && GameObject.Find("player") != null)
            {
                Instantiate(enemy, new Vector3(0, 11, 0), Quaternion.identity);
                enemyCounter++;
                yield return new WaitForSeconds(spawnTimer);
            }
            else yield return null;

        }
    }
}