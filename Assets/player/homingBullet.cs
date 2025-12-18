using UnityEngine;

public class homingBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletDamage = 40f;
    public float turnSpeed = 15f;   // Rotationsshastighet i grader per sekund
    
    private Transform target;

    private bool hasLock = true;

    void Start()
    {
        bulletDamage += (managerScript.bossesDestroyed * 10f);
        AcquireNearestEnemy();        
        Destroy(this.gameObject, 3f);
    }
    void AcquireNearestEnemy()
    {
        float bestDistSqr = Mathf.Infinity;
        Transform bestTarget = null;

        Vector3 currentPos = transform.position;
        Transform playerT = GameObject.FindGameObjectWithTag("Player")?.transform;
        Vector2 forwardFromPlayer = (playerT != null) ? (Vector2)playerT.up : (Vector2)transform.up;

        // Helper to consider a candidate only if it's in front of the player
        System.Action<GameObject> consider = (GameObject go) =>
        {
            if (go == null) return;
            Vector3 candidatePos = go.transform.position;

            // If player exists, check if candidate is in front of the player
            if (playerT != null)
            {
                Vector2 dirFromPlayer = (Vector2)(candidatePos - playerT.position);
                if (dirFromPlayer.sqrMagnitude <= 0.0001f) return;
                float dot = Vector2.Dot(forwardFromPlayer.normalized, dirFromPlayer.normalized);
                if (dot <= 0f) return; // behind or exactly perpendicular: ignore
            }

            float dSqr = (candidatePos - currentPos).sqrMagnitude;
            if (dSqr < bestDistSqr)
            {
                bestDistSqr = dSqr;
                bestTarget = go.transform;
            }
        };

        // Check regular enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies) consider(e);

        // Also check Dreadnoughts
        GameObject[] dreads = GameObject.FindGameObjectsWithTag("Dreadnought");
        foreach (GameObject d in dreads) consider(d);

        target = bestTarget;
        hasLock = (target != null);
    }

    void Update()
    {

        Transform playerT = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Try to acquire target if none
        if (target == null && (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 || GameObject.FindGameObjectsWithTag("Dreadnought").Length > 0))
        {
            AcquireNearestEnemy();
        }

        // If current target moved behind the player, try to retarget
        if (target != null && playerT != null)
        {
            Vector2 dirFromPlayerToTarget = (Vector2)target.position - (Vector2)playerT.position;
            if (dirFromPlayerToTarget.sqrMagnitude > 0.0001f)
            {
                float dot = Vector2.Dot(((Vector2)playerT.up).normalized, dirFromPlayerToTarget.normalized);
                if (dot <= 0f)
                {
                    // target is behind the player; attempt to find another
                    AcquireNearestEnemy();
                }
            }
        }

        if (target != null)
        {
            // Rotate smoothly toward target
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        // Move bullet forward along its local up (matches bulletLogic)
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        // Destroy if out of bounds
        if (transform.position.y < -10f || transform.position.y > 10f || transform.position.x < -20f || transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Damage enemy or dreadnought on hit (like bulletLogic)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyLogic Enemy = collision.gameObject.GetComponent<enemyLogic>();

            if (Enemy != null)
            {
                Enemy.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("Dreadnought"))
        {
            dreadnoughtLogic Dreadnought = collision.gameObject.GetComponent<dreadnoughtLogic>();

            if (Dreadnought != null)
            {
                Dreadnought.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
        }
    }
}
