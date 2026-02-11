using UnityEngine;

public class AutoShooting : MonoBehaviour
{
    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    private GameManager gameManager;

    [Header("Fire Rate Scaling")]
    public float startFireRate = 0.5f; 
    public float endFireRate = 0.1f;   

    [Header("Targeting")]
    public float detectionRange = 10f;
    private Transform targetEnemy;
    private float nextFireTime;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        FindClosestEnemy();

        if (targetEnemy != null)
        {
            // Rotate firePoint to face the enemy
            Vector2 direction = targetEnemy.position - firePoint.position;
            firePoint.right = direction;

            HandleShooting();
        }
    }

    void FindClosestEnemy()
    {
        // Find all objects tagged "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= detectionRange)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        targetEnemy = closestEnemy;
    }

    void HandleShooting()
    {
        if (gameManager == null) return;

        float difficulty = gameManager.DifficultyPercent;
        float currentFireRate = Mathf.Lerp(startFireRate, endFireRate, difficulty);

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + currentFireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}