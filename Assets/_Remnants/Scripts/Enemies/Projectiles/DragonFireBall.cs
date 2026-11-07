
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DragonFireball : MonoBehaviour
{
    #region Fireball Settings
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float lifetime = 5f;

    private Vector2 moveDirection;
    #endregion

    #region Component References
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;

        if (moveDirection.x > 0f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, transform.position);
            }

            Destroy(gameObject);
        }
    }

}
