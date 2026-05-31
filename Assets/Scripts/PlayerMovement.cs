using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {  get; private set; } 
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float duration = 0.15f;
    public float dashCooldown = 1f;
    public float invincibilityDuration = 0.2f;
   
    
    public float lungeSpeed = 3f;
    public float lungeDuration = 0.05f;
    
    public Tutorial tutorial;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection;
    private Animator animator;

    private bool isDashing = false;
    private bool isInvincible = false;
    private bool isLunging = false;
    private bool isKnockedBack = false;
    private float knockbackTimer = 0f;

    private float dashCooldownTimer = 0f;
    private float dashTimer = 0f;
    private float invincibilityTimer = 0f;
    private float lungeTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        lastMoveDirection = Vector2.right;

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 1") 
        { 
            tutorial.ShowMessage("Use WASD to move");
            tutorial.ShowMessage("Use LMB to attack");
            tutorial.ShowMessage("Press Space Bar to dash");
            tutorial.ShowMessage("Defeat enemies and collect the keycard to progress!");
           

        }

    }



    public bool IsInvincible => isInvincible;
    public Vector2 LastMoveDirection => lastMoveDirection;

    public void ApplyKnockback(float knockbackDuration)
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
    }

    public void AttackLunge()
    {
        if (isDashing || isKnockedBack) return;

        isLunging = true;
        lungeTimer = lungeDuration;

        rb.linearVelocity = lastMoveDirection * lungeSpeed;
    }
    void Update()
    {
        dashCooldownTimer -= Time.unscaledDeltaTime;

        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer < 0f)
            {
                isInvincible = false;
            }

        }

        if(isKnockedBack)
        {
            knockbackTimer -= Time.unscaledDeltaTime;
            if(knockbackTimer <= 0f)
            {
                isKnockedBack = false;
                rb.linearVelocity = Vector2.zero;
            }

            return;
        }

        if (isDashing)
        {
            dashTimer -= Time.unscaledDeltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                rb.linearVelocity = Vector2.zero;
            }

            return;
        }

        if (isLunging)
        {
            lungeTimer -= Time.unscaledDeltaTime;
            if(lungeTimer <= 0f)
            {
                isLunging = false;
                rb.linearVelocity = Vector2.zero;
            }
            return;
        }


        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W)) y += 1;
        if (Input.GetKey(KeyCode.S)) y -= 1;
        if (Input.GetKey(KeyCode.A)) x -= 1;
        if (Input.GetKey(KeyCode.D)) x += 1;

        moveInput = new Vector2(x, y).normalized;

       
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
        }

   
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f && lastMoveDirection != Vector2.zero)
        {
            isDashing = true;
            isInvincible = true;
            dashTimer = duration;
            invincibilityTimer = invincibilityDuration;
            dashCooldownTimer = dashCooldown;

            rb.linearVelocity = lastMoveDirection * dashSpeed;

            return;
        }

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
            animator.SetFloat("LastInputY", animator.GetFloat("InputY"));
        }

        
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void FixedUpdate()
    {
        if (!isDashing && !isKnockedBack && !isLunging)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }
}