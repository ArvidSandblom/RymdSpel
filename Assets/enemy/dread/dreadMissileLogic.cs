using Unity.VisualScripting;
using UnityEngine;

public class DreadMissileLogic : MonoBehaviour
{
    public float missileSpeed = 7f;
    public float missileDamage = 40f;
    public float turnSpeed = 60f;   // Rotationsshastighet i grader per sekund
    
    private Transform target;

    private bool hasLock = true;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;        
        
    }

    void Update()
    {
        if (hasLock && target != null)
        {
            // Calculate direction to player
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            
            // Rotate missile toward the target
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
            
            // Check if missile has passed the player (lost lock)
            direction.Normalize();
            float dot = Vector2.Dot(transform.right, direction);
            if (dot < 0f)
            {
                hasLock = false; // Lost lock
            }
            
            // Check collision with player
            if (Vector2.Distance(transform.position, target.position) < 0.5f)
            {
                Destroy(gameObject);
            }
        }

        // Move missile forward
        transform.Translate(Vector2.right * missileSpeed * Time.deltaTime);
        
        // Destroy if out of bounds
        if (transform.position.y < -10f || transform.position.y > 10f || transform.position.x < -20f || transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript player = collision.gameObject.GetComponent<playerScript>();
            if (player != null)
            {
                player.TakeDamage(missileDamage);
            }
            Destroy(gameObject);
        }
    }
}