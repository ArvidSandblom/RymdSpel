using Unity.VisualScripting;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float bulletSpeed = 2.5f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}
