using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    #region Component References
    [SerializeField] private RectTransform healthBarMiddle;
    [SerializeField] private RectTransform healthBarRight;
    [SerializeField] private RectTransform healthFillLeft;
    [SerializeField] private RectTransform healthFillMiddle;
    [SerializeField] private RectTransform healthFillRight;
    #endregion

    #region Health Bar Settings
    [SerializeField] private float widthPerHealth = 1f;

    private float originalBarMiddleWidth;
    private float originalBarRightX;
    private float originalFillMiddleWidth;
    private float originalFillRightX;
    private int originalMaxHealth;
    #endregion

    private void Awake()
    {
        originalBarMiddleWidth = healthBarMiddle.rect.width;
        originalBarRightX = healthBarRight.anchoredPosition.x;

        originalFillMiddleWidth = healthFillMiddle.rect.width;
        originalFillRightX = healthFillRight.anchoredPosition.x;
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (originalMaxHealth == 0)
        {
            originalMaxHealth = maxHealth;
        }

        // Calculate additional width from maximum health upgrades
        float extraHealth = maxHealth - originalMaxHealth;
        float extraWidth = extraHealth * widthPerHealth;

        // Expand the outer frame
        float newBarMiddleWidth = originalBarMiddleWidth + extraWidth;
        healthBarMiddle.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newBarMiddleWidth);

        float newBarRightX = originalBarRightX + extraWidth;
        healthBarRight.anchoredPosition = new Vector2(newBarRightX, healthBarRight.anchoredPosition.y);

        // Calculate the visible fill width based on current health
        float fullFillWidth = originalFillMiddleWidth + extraWidth;
        float healthPercentage = Mathf.Clamp01((float)currentHealth / maxHealth);

        float totalFillWidth = healthFillLeft.rect.width + fullFillWidth + healthFillRight.rect.width;
        float visibleFillWidth = totalFillWidth * healthPercentage;

        float leftWidth = healthFillLeft.rect.width;
        float rightWidth = healthFillRight.rect.width;

        // Left cap stays at its original size
        healthFillLeft.gameObject.SetActive(visibleFillWidth > 0f);

        // Middle section shrinks or grows based on health
        float visibleMiddleWidth = Mathf.Clamp(visibleFillWidth - leftWidth, 0f, fullFillWidth);
        healthFillMiddle.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, visibleMiddleWidth);
        healthFillMiddle.gameObject.SetActive(visibleMiddleWidth > 0f);

        // Right cap follows the visible middle section
        float newFillRightX = healthFillMiddle.anchoredPosition.x + visibleMiddleWidth;
        healthFillRight.anchoredPosition = new Vector2(newFillRightX, healthFillRight.anchoredPosition.y);

        // Only show the right cap when the bar is sufficiently filled
        healthFillRight.gameObject.SetActive(visibleFillWidth >= leftWidth + rightWidth);
    }
}