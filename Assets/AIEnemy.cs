using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public float speed;
    public float distanceBetween;
    private float distance;
    private Transform player;
    private Animator animator;

    void Start()
    {
        player = PlayerMovement.Instance.transform;
        animator= GetComponent<Animator>();
    }

    void Update()
    {
        if (PlayerMovement.Instance == null) return;

        distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = player.position - transform.position;
        direction.Normalize();

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        Vector3 scale = transform.localScale;
        if (player.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
              if(transform.hasChanged)
            {
            animator.SetBool("isWalking", true);
            transform.hasChanged = false;
            }
            else{
                animator.SetBool("isWalking", false);
            }


    }
    
}