using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    public GameObject bullet;
    private bool spawned = false;
    private float decay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left *  playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * playerSpeed * Time.deltaTime);
        }
        if (transform.position.x <= -11)
        {
            transform.position = new Vector3(10.9f, transform.position.y, 0);
        }
        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-10.9f, transform.position.y, 0);
        }
        if (transform.position.y <= -5)
        {
            transform.position = new Vector3(transform.position.x, 4.9f, 0);
        }
        if (transform.position.y >= 5)
        {
            transform.position = new Vector3(transform.position.x, -4.9f, 0);
        }
        Reset();
        if (Input.GetKey(KeyCode.Space) && !spawned)
        {
            Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0), this.transform.rotation);
            decay = 1f;
            spawned = true;
        }
    }
    private void Reset()
    {
        if (spawned && decay > 0)
        {
            decay -= Time.deltaTime;
            
        }
        if (decay < 0)
        {
            decay = 0;
            spawned = false;
        }
    }
}
