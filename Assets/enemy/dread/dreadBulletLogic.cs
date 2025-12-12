using UnityEngine;

public class dreadBulletLogic : MonoBehaviour
{
    public float bulletDamage = 20f;
    public float bulletSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 5f);        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript player = collision.gameObject.GetComponent<playerScript>();

            if (player != null)
            {
                player.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
        }
    }
}
