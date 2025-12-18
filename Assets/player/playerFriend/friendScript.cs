using UnityEngine;

public class friendScript : MonoBehaviour
{
    public GameObject bullet;
    private float moveSpeed = 4f;
    private float fireTimer = 0f;
    public float fireRate = 0.25f;
    public float damage = 10f;
    private int direction = 0;
    private float directionTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = Random.value > 0.5f ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
        directionTimer += Time.deltaTime;
        if (fireTimer <= 0f)
        {
            GameObject thisBullet = Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0), this.transform.rotation);
            if (thisBullet != null)
            {
                if (thisBullet.TryGetComponent<bulletLogic>(out var bl)) bl.bulletDamage = damage;
                else if (thisBullet.TryGetComponent<friendBulletLogic>(out var fbl)) fbl.bulletDamage = damage;
            }
            fireTimer = fireRate;
        }

        if (directionTimer >= 4f)
        {
            direction = Random.value > 0.5f ? 1 : -1;
            directionTimer = 0;
        }

        if (transform.position.x <= -13f)
        {
            direction = 1;
        }
        else if (transform.position.x >= 13f)
        {
            direction = -1;
        }
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * direction);
    
    }
}
