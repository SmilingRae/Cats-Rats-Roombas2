using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public int damage = 8;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyHealth enemy = collider.GetComponent<EnemyHealth>();

        if(enemy != null)
        {
            Vector2 direction = (collider.transform.position - transform.root.position).normalized;
            enemy.TakeDamage(damage, direction);
        }
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
