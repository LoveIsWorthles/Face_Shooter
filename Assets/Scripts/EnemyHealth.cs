using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 3f; // Takes 3 hits to die

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // You could add an explosion effect or sound here!
        Destroy(gameObject);
    }
}