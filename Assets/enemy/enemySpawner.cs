using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    public GameObject enemy;
    private int enemyCounter;
    private GameObject player;
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    void Update()
    {

    }

    //IEnumerator is a type of Coroutine that is able to execute commands outside of update. This
    //particular IEnumerator repeats the code within the while-loop forever -- and under normal circumstances
    //this could be dangerous and crash the application. However, in the below example it only executes
    //the code once per 5 seconds. A coroutine must always have a "yield".
    IEnumerator EnemySpawner()
    {
        while (true)
        {
            if (enemyCounter < 3 && GameObject.Find("Player") != null)
            {
                Instantiate(enemy, new Vector3(0, 11, 0), Quaternion.Euler(0, 0, 180));

                enemyCounter++;


                yield return new WaitForSeconds(5);
            }
            else yield return null; 


        }
    }
}