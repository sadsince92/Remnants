
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Health Settings
    [SerializeField] private int maxHealth = 40;
    private int currentHealth;
    #endregion

    #region Component References
    private Animator anim;
    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("hurt");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

