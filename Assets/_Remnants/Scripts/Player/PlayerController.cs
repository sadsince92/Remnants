using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Movement Settings
    // -- MOVEMENT -- //
    [SerializeField] private float moveSpeed = 5f;
    private float moveInput;
    #endregion

    #region Jump Settings
    // -- JUMP -- //
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private float fallGravityMultiplier = 1.5f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    private bool jumpCutRequested;
    private float baseGravity;
    private float jumpBufferCounter;
    #endregion

    #region Dash Settings
    // -- DASH -- // 
    [SerializeField] private float dashSpeed = 12f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private GameObject dashDustPrefab;
    [SerializeField] private Vector2 dashDustOffset = new Vector2(0f, -0.5f);
    [SerializeField] private float groundDashCooldown = 0.5f;
    private bool isDashing;
    private bool airDashAvailable = true;
    private float dashTimer;
    private float dashDirection;
    private float groundDashCooldownTimer;
    #endregion

    #region Knockback Settings
    // -- KNOCKBACK -- //
    [SerializeField] private float knockbackForce = 8f;
    [SerializeField] private float knockbackDuration = 0.2f;
    private bool isKnockedBack;
    private float knockbackTimer;
    #endregion
    #region Attack Settings
    // -- ATTACK -- //
    private bool isAttacking;
    #endregion

    #region Ground Check Settings
    // -- GROUND CHECK -- //
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;
    private bool isGrounded;
    #endregion

    #region Component References
    // -- CHARACTER COMPONENTS -- //
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private CapsuleCollider2D col;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<CapsuleCollider2D>();

        baseGravity = rb.gravityScale;
    }
    private void Update()
    {
        moveInput = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
        moveInput = -1f;
        //Debug.Log("Moving Left");
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
        moveInput = 1f;
        //Debug.Log("Moving Right");
        }

        if (!isDashing)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                jumpBufferCounter = jumpBufferTime;
            }
            if (Keyboard.current.spaceKey.wasReleasedThisFrame)
            {
                jumpCutRequested = true;
            }
        }
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && !isDashing && !isKnockedBack)
        {
            if (isGrounded && groundDashCooldownTimer <= 0f)
            {
                StartDash();
            }
            else if (!isGrounded && airDashAvailable)
            {
                StartDash();
            }
        }
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }
        if (groundDashCooldownTimer > 0f)
        {
            groundDashCooldownTimer -= Time.deltaTime;
        }
        // Attack Input
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (isGrounded && !isDashing && !isKnockedBack && !isAttacking)
            {
                StartAttack();
            }
        }
        FlipSprite();

        UpdateAnimation();
        jumpBufferCounter -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        CheckGrounded();

        // Coyote Time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }

        if (isKnockedBack)
        {
            knockbackTimer -= Time.fixedDeltaTime;

            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
            }
            else
            {
                ApplyFallGravity();
                return;
            }
        }

        if (isDashing)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);
            return;
        }

        if (isAttacking)
        {
            rb.linearVelocityX = 0f;
            ApplyFallGravity();
            return;
        }

        rb.linearVelocityX = moveInput * moveSpeed;

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }
        if (jumpCutRequested)
        {
            JumpCut();
            jumpCutRequested = false;
        }

        ApplyFallGravity();
    }
    private void FlipSprite()
    {
        // Preventing Angie from changing directions while mid attack
        if (isDashing || isAttacking) return;

        if (moveInput < 0f)
        {
            sr.flipX = true;
        }
        else if (moveInput > 0f)
        {
            sr.flipX = false;
        }
    }
    private void UpdateAnimation()
    {
        anim.SetBool("isMoving", moveInput != 0f && !isAttacking);
        anim.SetBool("isGrounded", isGrounded);
    }
    private void StartAttack()
    {
        isAttacking = true;
        anim.SetTrigger("attack1");
    }
    public void EndAttack()
    {
        //Debug.Log("EndAttack called!");
        isAttacking = false;
    }
    private void CheckGrounded()
    {
        bool wasGrounded = isGrounded;

        Bounds bounds = col.bounds;

        Vector2 boxCenter = new Vector2(bounds.center.x, bounds.min.y - groundCheckDistance / 2f);
        Vector2 boxSize = new Vector2(bounds.size.x * 0.9f, groundCheckDistance);

        isGrounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer) != null;

        if (isGrounded && !wasGrounded)
        {
            airDashAvailable = true;
        }
    }
    private void Jump()
    {
        rb.linearVelocityY = jumpForce;
    }
    private void JumpCut()
    {
        if (rb.linearVelocityY > 0f)
        {
            rb.linearVelocityY *= jumpCutMultiplier;
        }
        //Debug.Log("Jump Cut Activated!");
    }

    private void StartDash()
    {
        isDashing = true;

        if (isGrounded)
        {
            groundDashCooldownTimer = groundDashCooldown;
        }
        else
        {
            airDashAvailable = false;
        }

        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
        jumpCutRequested = false;

        dashTimer = dashDuration;

        dashDirection = sr.flipX ? -1f : 1f;

        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);

        anim.SetBool("isDashing", true);

        Vector3 dustPosition = transform.position + new Vector3(-dashDirection * dashDustOffset.x, dashDustOffset.y, 0f);
        GameObject dust = Instantiate(dashDustPrefab, dustPosition, Quaternion.identity);
        dust.GetComponent<SpriteRenderer>().flipX = dashDirection < 0f;
    }

    private void EndDash()
    {
        isDashing = false;
        //Debug.Log("Dash ended!");
        rb.gravityScale = baseGravity;
        rb.linearVelocityX = 0f;

        anim.SetBool("isDashing", false);
    }

    public void ApplyKnockback(Vector2 damageSourcePosition)
    {
        // Which side of the enemy Angie is on
        float knockbackDirection;

        if (isDashing)
        {
            knockbackDirection = -dashDirection;
        }
        else
        {
            knockbackDirection = transform.position.x < damageSourcePosition.x ? -1f : 1f;
        }
        if (isDashing)
        {
            EndDash();
        }

        isKnockedBack = true;
        knockbackTimer = knockbackDuration;

        // Clear pending jumps
        jumpBufferCounter = 0f;
        jumpCutRequested = false;

        // Apply horizontal knockback without pushing Angie upwards
        rb.linearVelocity = new Vector2(knockbackDirection * knockbackForce, Mathf.Min(rb.linearVelocityY, 0f));
    }
    private void ApplyFallGravity()
    {
        if (!isGrounded && rb.linearVelocityY < 0f)
        {
            rb.gravityScale = baseGravity * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
}

