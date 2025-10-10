using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bigLaser : MonoBehaviour
{
    public float laserDuration = 5f;
    public float laserDamage = 100f;
    private float timer = 0f;
    private bool isActive = false;
    public GameObject laserBeam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ActivateLaser());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ActivateLaser()
    {
        isActive = true;
        laserBeam.SetActive(true);
        timer = 0f;
        while (timer < laserDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        laserBeam.SetActive(false);
        isActive = false;
    }
}
