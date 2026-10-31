using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    #region Health Settings
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    #endregion

    #region Component References
    [SerializeField] private HealthBarUI healthBarUI;
    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
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
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
    }
}