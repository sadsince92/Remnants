using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Movement Settings
    [SerializeField] private float moveSpeed = 5f;
    private float moveInput;
    #endregion

    #region Jump Settings
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private float fallGravityMultiplier = 1.5f;
    private bool jumpRequested;
    private bool jumpCutRequested;
    private float baseGravity;
    #endregion

    #region Ground Check Settings
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;
    private bool isGrounded;
    #endregion

    #region Component References
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

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
        jumpRequested = true;
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            jumpCutRequested = true;
        }

        FlipSprite();
        UpdateAnimation();
    }
    private void FixedUpdate()
    {
        CheckGrounded();

        rb.linearVelocityX = moveInput * moveSpeed;

        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
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
        anim.SetBool("isMoving", moveInput != 0f);
        anim.SetBool("isGrounded", isGrounded);
    }
    private void CheckGrounded()
    {
        Bounds bounds = col.bounds;

        Vector2 boxCenter = new Vector2(bounds.center.x, bounds.min.y - groundCheckDistance / 2f);
        Vector2 boxSize = new Vector2(bounds.size.x * 0.9f, groundCheckDistance);

        isGrounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer) != null;
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
    private void ApplyFallGravity()
    {
        if (rb.linearVelocityY < 0f)
        {
            rb.gravityScale = baseGravity * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
}

