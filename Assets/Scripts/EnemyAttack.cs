using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 15;
    public float attackCooldown = 1f;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;

    public GameObject hitParticlePrefab;
    public float hitStopDuration = 0.05f;
    public Color damageFlashColor = Color.red;
    public float flashDuration = 0.1f;

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

            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if(rb == null) rb = collider.GetComponentInParent <Rigidbody2D>();
            if(rb != null)
            {
                Vector2 knockbackDir = (collider.transform.position - transform.position).normalized;

                PlayerMovement playerMove = rb.GetComponent<PlayerMovement>();
                if (playerMove != null)
                {
                    playerMove.ApplyKnockback(knockbackDuration);
                }
                rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            }

            if(hitParticlePrefab != null)
            {
                Vector2 contactPoint = collider.ClosestPoint(transform.position);
                Instantiate(hitParticlePrefab, contactPoint, Quaternion.identity);
            }

            StartCoroutine(HitStopRoutine());

            SpriteRenderer playerSprite = collider.GetComponentInChildren<SpriteRenderer>();
            if (playerSprite != null)
            {
                StartCoroutine(FlashRoutine(playerSprite));
            }

            if(CameraShake.Instance != null)
            {
                CameraShake.Instance.Shake(0.2f);
            }
        }
    }

    private IEnumerator HitStopRoutine()
    {
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(hitStopDuration);

        Time.timeScale = 1f;
    }

    private IEnumerator FlashRoutine(SpriteRenderer sprite)
    {
        sprite.color = damageFlashColor;

        yield return new WaitForSecondsRealtime(flashDuration);

        sprite.color = Color.white;
    }
}
