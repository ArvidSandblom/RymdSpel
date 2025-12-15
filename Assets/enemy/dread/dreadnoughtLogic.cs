using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dreadnoughtLogic : MonoBehaviour
{
    public Vector2 targetPosition;
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
    float phaseTimer = 0f;
    private int dreadBulletPhase;
    private float dreadWaitBetweenShots;

    [SerializeField]
    bool phase1 = false;
    [SerializeField]
    bool phase2 = false;
    [SerializeField]
    bool phase3 = false;
    [SerializeField]
    bool phase4 = false;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GameObject.Find("dreadHealthGreen").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(dreadnoughtBullet());
        //DreadnoughtMovement();
        StartCoroutine(dreadnoughtMissile());
        healthBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {        
        timer += Time.deltaTime;  
        phaseTimer += Time.deltaTime; 
        DreadnoughtMovement();
        
    }
    IEnumerator dreadnoughtMissile()
    {
        while (this.gameObject != null && dreadHealth > 0)
        {
            GameObject thisMissile;
            Vector3 playerPos = player.transform.position;
            Vector3 direction = playerPos - (Vector3)this.transform.position;
            direction.Normalize();
            thisMissile = Instantiate(dreadMissile, new Vector3(this.transform.position.x, this.transform.position.y - 1f, 0), Quaternion.Euler(0, 0, Mathf.Atan2(direction.x, direction.y*-1)));
            yield return new WaitForSeconds(dreadFireRate * 3);
        }
        yield return null;
    }
    IEnumerator dreadnoughtBullet()
    {
        while (this.gameObject != null && dreadHealth > 0)
        {
            if (phase1)
            {
                dreadBulletPhase = -1;
                dreadWaitBetweenShots = 0.25f;
            }
            else if (phase2)
            {
                dreadBulletPhase = -2;
                dreadWaitBetweenShots = 0.15f;
            }
            else if (phase3)
            {
                dreadBulletPhase = -3;
                dreadWaitBetweenShots = 0.1f;
            }
            else if (phase4)
            {
                dreadBulletPhase = -8;
                dreadWaitBetweenShots = 0.05f;
            }
                        
            for (int i = dreadBulletPhase; i <= 1; i++)
            {                
                
                Vector3 playerPos = player.transform.position;
                Vector3 direction = playerPos - (Vector3)this.transform.position;
                direction.Normalize();
                GameObject thisBullet;
                thisBullet = Instantiate(dreadBullet, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, 0), Quaternion.Euler(0, 0, Mathf.Atan2(direction.x, direction.y*-1) * Mathf.Rad2Deg + (i * 15f)));
                yield return new WaitForSeconds(dreadWaitBetweenShots);
            }        
            yield return new WaitForSeconds(dreadFireRate);
        }
        yield return null;
    }
    void DreadnoughtMovement()
    {
        if (phaseTimer >= 40f)
            {
                phase1 = false;
                phase2 = false;
                phase3 = false;
                phase4 = true;
            }
        else if (dreadHealth <= dreadMaxHealth * 0.25f || phaseTimer >= 30f )
            {
                phase1 = false;
                phase2 = false;
                phase3 = true;
            }
        else if (dreadHealth <= dreadMaxHealth * 0.5 || phaseTimer >= 20f)
            {
                phase1 = false;
                phase2 = true;
                phase3 = false;
            }
        else if (dreadHealth <= dreadMaxHealth * 0.75 || phaseTimer >= 10f)
            {
                phase1 = true;
                phase2 = false;
                phase3 = false;
            }        

        if (phase1 && timer >= 3f)
        {
            targetPosition = new Vector2(Random.Range(-10f,10f), Random.Range(8f,5f)); 
            timer = 0f;               
        }
        else if (phase2 && timer >= 2f)
        {
            dreadMoveSpeed = 3f;
            targetPosition = new Vector2(Random.Range(-10f,10f), Random.Range(5f,3f));
            timer = 0f;
        }
        else if (phase3 && timer >= 1f)
        {
            dreadMoveSpeed = 4f;
            targetPosition = new Vector2(Random.Range(-10f,10f), Random.Range(3f,0f));
            timer = 0f;
        }
        else if (phase4)
        {
            dreadMoveSpeed = 2f;
            targetPosition = new Vector2(transform.position.x, -10f);
        }
        
        // Hade inte .normalize här tidigare vilket gjorde att den rörde sig snabbare diagonalt :|
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.Translate(direction * dreadMoveSpeed * Time.deltaTime);
        
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
