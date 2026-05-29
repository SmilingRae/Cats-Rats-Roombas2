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
    public Tutorial tutorial;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection;
    private Animator animator;

    private bool isDashing = false;
    private bool isInvincible = false;

    private float dashCooldownTimer = 0f;
    private float dashTimer = 0f;
    private float invincibilityTimer = 0f;

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
    void Update()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer < 0f)
            {
                isInvincible = false;
            }

        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
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
        if (!isDashing)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }
}