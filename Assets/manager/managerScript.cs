using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class managerScript : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator powerupSpawner()
    {
        
        while (true)
        {
            if (playerScript.isPlayerAlive)
            {

            }
        }
        
        yield return new WaitForSeconds(10);
    }
}
