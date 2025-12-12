using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dreadnoughtLogic : MonoBehaviour
{
    private Vector2 targetPosition;
    public float dreadMoveSpeed = 2f;
    public float dreadHealth = 300f;
    public float dreadMaxHealth = 300f;
    private Image healthBar;
    public float dreadDamage = 30f;
    public float dreadFireRate = 2f;
    private GameObject player;
    public GameObject dreadBullet;
    public GameObject dreadMissile;
    float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GameObject.Find("dreadHealthGreen").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(dreadnoughtBullet());
        StartCoroutine(DreadnoughtMovement());
        healthBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {        
        timer += Time.deltaTime;   
        transform.Translate(targetPosition * dreadMoveSpeed * Time.deltaTime);
    }
    IEnumerator dreadnoughtMissile()
    {
        while (this.gameObject != null && dreadHealth > 0)
        {
            GameObject thisMissile;
            thisMissile = Instantiate(dreadMissile, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(dreadFireRate * 3);
        }
        yield return null;
    }
    IEnumerator dreadnoughtBullet()
    {
        while (this.gameObject != null && dreadHealth > 0)
        {
            //3 Bullets with spread 20 degrees
            //Kolla logiken här igen
            for (int i = -1; i <= 1; i++)
            {                
                GameObject thisBullet;                
                thisBullet = Instantiate(dreadBullet, player.transform.position - transform.position, Quaternion.Euler(0, 0, i * 20));
            }        
            yield return new WaitForSeconds(dreadFireRate);
        }
        yield return null;
    }
    IEnumerator DreadnoughtMovement()
    {
        bool phase1 = false;
        bool phase2 = false;
        bool phase3 = false;
        bool phase4 = false;
        while (this.gameObject != null && dreadHealth > 0)
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
            if (phase1 && timer >= 3f)
            {
                targetPosition = new Vector2(Random.Range(-10f,10f), Random.Range(8f,5f));                
            }
            else if (phase2 && timer >= 2f)
            {
                dreadMoveSpeed = 3f;
                targetPosition = new Vector2(transform.position.x + Random.Range(-10f,10f), transform.position.y + Random.Range(5f,3f));
            }
            else if (phase3 && timer >= 1f)
            {
                dreadMoveSpeed = 4f;
                targetPosition = new Vector2(transform.position.x + Random.Range(-10f,10f), transform.position.y + Random.Range(3f,0f));
            }
            else if (phase4)
            {
                dreadMoveSpeed = 2f;
                targetPosition = new Vector2(transform.position.x, -10f);
            }    
            
            yield return null;
        }
        yield return null;        
        
    }
    public void TakeDamage(float damage)
    {
        dreadHealth -= damage;
        healthBar.fillAmount = dreadHealth / dreadMaxHealth;        
        if (dreadHealth <= 0)
        {
            
            Destroy(this.gameObject);
        }
    }
}
