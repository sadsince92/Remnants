using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    #region Health Settings
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    #endregion

    #region Invincibility Settings
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float flashInterval = 0.1f;
    private float invincibilityTimer;
    #endregion

    #region Component References
    [SerializeField] private HealthBarUI healthBarUI;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (invincibilityTimer > 0f)
        {
            invincibilityTimer -= Time.deltaTime;

            spriteRenderer.enabled =
                Mathf.FloorToInt(invincibilityTimer / flashInterval) % 2 == 0;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    /* private void Update()
    {
        // Press H to take 20 damage
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            TakeDamage(20);
        }

        // Press J to heal 20 health
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            Heal(20);
        }
    } */

    public void TakeDamage(int damage)
    {
        if (invincibilityTimer > 0f)
        {
            return;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
        invincibilityTimer = invincibilityDuration;

        animator.SetBool("isHurt", true);
        animator.SetTrigger("Hurt");
    }
    public void EndHurt()
    {
        animator.SetBool("isHurt", false);
    }
}