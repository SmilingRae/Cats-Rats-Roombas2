using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int totalHealth = 20;
    public int health;

    public float knockbackForce = 8f;
    public float knockbackDuration = 0.15f;

    private Rigidbody2D rb;

    private Vector2 knockbackVelocity;
    private float knockbackTimer = 0f;

    void Start()
    {
        health = totalHealth;
        rb = GetComponent<Rigidbody2D>();
    }


    public void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        health = health - amount;
        if(health < 0) health = 0;
        
        knockbackVelocity = knockbackDirection * knockbackForce;
        knockbackTimer = knockbackDuration;

        if(health <= 0)
        {
            Die();
        }

        
    }

    public void Die()
    {
        Destroy(gameObject);
    }

 
    void Update()
    {
        if (knockbackTimer > 0f)
        {
            rb.linearVelocity = knockbackVelocity;
            knockbackTimer -= Time.deltaTime;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
