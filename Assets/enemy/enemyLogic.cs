using UnityEngine;

public class enemyLogic : MonoBehaviour
{
    public float enemySpeed = 3f;
    public float speedDown = 0.5f;
    public float minX = -8f;
    public float maxX = 8f;
    public float fireRate = 0.75f;
    public float fireTimer = 0f;
    public GameObject enemyBullet;

    private int direction = Random.Range(1, -1);
    private float changeDirectionTime;
    private float timer;
    public float health = 100f;

    void Start()
    {
        SetRandomDirectionTime();
    }
        

    void Update()
    {
        if (health <= 0f)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.right * enemySpeed * direction * Time.deltaTime);
        transform.Translate(Vector3.down * speedDown * Time.deltaTime);
        
        if (transform.position.x <= minX)
        {
            direction = 1;
            SetRandomDirectionTime();
            timer = 0f;
        }
        else if (transform.position.x >= maxX)
        {
            direction = -1;
            SetRandomDirectionTime();
            timer = 0f;
        }

        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            direction = Random.value > 0.5f ? 1 : -1;
            SetRandomDirectionTime();
            timer = 0f;
        }
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Instantiate(enemyBullet, new Vector3(this.transform.position.x, this.transform.position.y + -0.5f, 0), Quaternion.Euler(0, 0, 180));
            fireTimer = fireRate;
            
        }
    }
    void SetRandomDirectionTime()
    {
        changeDirectionTime = Random.Range(0.5f, 2f);
    }
}

