
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject hitbox;
    public float attackDuration = 0.2f;
    public float attackDistance = 1f;

    private float attackTimer = 0f;
    private bool isAttacking = false;

    private PlayerMovement movement;
    private Animator animator;
    AudioManager audioManager;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        hitbox.SetActive(false);
         audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            attackTimer = attackDuration;

            Vector2 dir = movement.LastMoveDirection;
            audioManager.PlaySFX(audioManager.attack);
            if(dir == Vector2.zero)
            {
                dir = Vector2.right;
            }

            if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                dir = new Vector2(Mathf.Sign(dir.x), 0);
            }
            else
            {
                dir = new Vector2(0, Mathf.Sign(dir.y));
            }

            int attackDir = 0;

            if (dir.x > 0) attackDir = 3;
            else if (dir.x < 0) attackDir = 2;
            else if (dir.y > 0) attackDir = 1;
            else if (dir.y < 0) attackDir = 0;

            animator.SetInteger("AttackDir", attackDir);
            animator.SetTrigger("Attack");

            hitbox.transform.localPosition = dir * attackDistance;

            hitbox.SetActive(true);
        }

        if (isAttacking)
        {
            attackTimer = attackTimer - Time.deltaTime;
            if(attackTimer <= 0f)
            {
                isAttacking=false;
                hitbox.SetActive(false);
            }
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        hitbox.SetActive(false);
    }
}
