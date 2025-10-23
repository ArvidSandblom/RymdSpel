using Unity.VisualScripting;
using UnityEngine;

public class bulletLogic : MonoBehaviour
{
    public float bulletDamage = 0f;
    public float bulletSpeed = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Destroy(this.gameObject, 3f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyLogic Enemy = collision.gameObject.GetComponent<enemyLogic>();

            if (Enemy != null)
            {
                Enemy.TakeDamage(bulletDamage);                
            }
            Destroy(gameObject);

        }
    }


}
