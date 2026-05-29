using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 15;
    public float attackCooldown = 1f;

    private float attackTimer = 0f;

    void Start()
    {
        
    }

    
    void Update()
    {
        attackTimer = attackTimer - Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        

        if (attackTimer > 0f) return;

        PlayerHealth player = collider.GetComponent<PlayerHealth>();

        if (player == null)
        {
            player = collider.GetComponentInParent<PlayerHealth>();
        }
        

        if (player != null)
        {
            
            player.TakeDamage(damage);
            attackTimer = attackCooldown;
        }
    }
}
