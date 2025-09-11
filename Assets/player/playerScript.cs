using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float playerSpeed = 7.5f;
    public GameObject bullet;
    private float fireTimer = 0f;
    public float fireRate = 0.5f;
    public float maxHealth, currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        /*if (currentHealth <= 0f)
        {
            Destroy(gameObject);
        }*/
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y <= 0)
            {
                transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);
            }
            else if (transform.position.y >= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
            }
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y >= -4.5f)
            {
                transform.Translate(Vector3.down * playerSpeed * Time.deltaTime);
            }
            else if (transform.position.y <= -4.5)
            {
                transform.position = new Vector3(transform.position.x, -4.5f, 0);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        if (transform.position.x <= -11)
        {
            transform.position = new Vector3(10.9f, transform.position.y, 0);
        }
        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-10.9f, transform.position.y, 0);
        }
        fireTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), this.transform.rotation);
            fireTimer = fireRate;
        }
    }
}