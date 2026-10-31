using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    #region Component References
    [SerializeField] private Image healthFill;
    #endregion

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthFill.fillAmount = (float)currentHealth / maxHealth;
    }
}