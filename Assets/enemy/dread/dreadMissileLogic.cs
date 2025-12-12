using UnityEngine;

public class DreadMissileLogic : MonoBehaviour
{
    public float missileSpeed = 4f;
    public float missileDamage = 40f;
    public float turnSpeed = 120f;   // Rotationsshastighet i grader per sekund
    
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
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            direction.Normalize();

            // Avgöra var spelaren är i förhållande till missilen
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Istället för att sätta rotationen direkt, rotera gradvis mot målet
            float angle = Mathf.MoveTowardsAngle(
                transform.eulerAngles.z,
                targetAngle,
                turnSpeed * Time.deltaTime
            );

            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Missilen ska förlora låsningen om den har passerat spelaren
            float dot = Vector2.Dot(transform.right, direction);
            if (dot < 0f)
            {
                hasLock = false; // Lost lock
            }
        }

        // Annars ska den fortsätta rakt fram
        transform.Translate(Vector2.right * missileSpeed * Time.deltaTime);
        if (transform.position.y < -10f || transform.position.y > 10f || transform.position.x < -20f || transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
    }
}