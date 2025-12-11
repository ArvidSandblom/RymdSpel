using System.Collections;
using UnityEngine;

public class dreadnoughtLogic : MonoBehaviour
{
    private Vector2 targetPosition;
    public float dreadMoveSpeed = 2f;
    public float dreadHealth = 300f;
    public float dreadDamage = 30f;
    public float dreadFireRate = 2f;
    private GameObject player;
    public GameObject dreadBullet;
    public GameObject dreadMissile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DreadnoughtShooting());
        StartCoroutine(DreadnoughtMovement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DreadnoughtShooting()
    {
        while (dreadHealth > 0)
        {
            // Shooting logic here
            yield return new WaitForSeconds(dreadFireRate);
        }
    }
    IEnumerator DreadnoughtMovement()
    {
        float timer = 0f;
        timer =+ Time.deltaTime;
        bool phase1 = false;
        bool phase2 = false;
        bool phase3 = false;
        bool phase4 = false;
        while (dreadHealth > 0)
        {
            if (dreadHealth >= 200f && timer >= 10f)
            {
                phase1 = true;
                phase2 = false;
                phase3 = false;
            }
            else if (dreadHealth <= 200f && dreadHealth >= 100f && timer >= 20f)
            {
                phase1 = false;
                phase2 = true;
                phase3 = false;
            }
            else if (dreadHealth < 100f && timer >= 30f)
            {
                phase1 = false;
                phase2 = false;
                phase3 = true;
            }
            if (timer >= 40f)
            {
                phase1 = false;
                phase2 = false;
                phase3 = false;
                phase4 = true;

            }
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, dreadMoveSpeed * Time.deltaTime);
            if (phase1 && timer >= 3f)
            {
                targetPosition = new Vector2(Random.Range(-5f,5f), Random.Range(-1f,1f));                
            }
            else if (phase2 && timer >= 2f)
            {
                dreadMoveSpeed = 3f;
                targetPosition = new Vector2(transform.position.x + Random.Range(-7f,7f), transform.position.y + Random.Range(-1.5f,1.5f));
            }
            else if (phase3 && timer >= 1f)
            {
                dreadMoveSpeed = 4f;
                targetPosition = new Vector2(transform.position.x + Random.Range(-10f,10f), transform.position.y + Random.Range(-2f,2f));
            }
            else if (phase4)
            {
                dreadMoveSpeed = 2f;
                targetPosition = new Vector2(transform.position.x, -10f);
            }      
        }
        yield return null;
        
    }
    public void TakeDamage(float damage)
    {
        dreadHealth -= damage;
        if (dreadHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
