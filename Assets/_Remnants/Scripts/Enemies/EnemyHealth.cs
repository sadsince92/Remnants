using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    #region Health Settings
    [SerializeField] private int maxHealth = 40;
    private int currentHealth;
    #endregion

    #region Death Settings
    [SerializeField] private float deathFadeDuration = 0.5f;
    #endregion

    #region Component References
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        anim.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(DeathFade());
    }

    private IEnumerator DeathFade()
    {
        // Disable colliders so the enemy can no longer interact
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }

        // Keep the Hurt animation playing during the fade
        anim.SetBool("isDead", true);

        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsed < deathFadeDuration)
        {
            elapsed += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, elapsed / deathFadeDuration);

            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }
}
