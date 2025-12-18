using Unity.VisualScripting;
using UnityEngine;

public class enemyBullet : MonoBehaviour

{
    public float damage = 20f;
    public float bulletSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        Destroy(gameObject, 5f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript player = collision.gameObject.GetComponent<playerScript>();
            if (player != null)
            {
                if (player.GetComponent<playerScript>().dodgeChance < Random.Range(0, 101))
                {                    
                    player.TakeDamage(damage);
                }

            }
            Destroy(gameObject);

        }
    }


}
