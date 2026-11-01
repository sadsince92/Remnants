using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    #region Component References
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    #endregion

    private void Awake()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        playerController = GetComponentInParent<PlayerController>();
    }

    public void EndHurt()
    {
        playerHealth.EndHurt();
    }

    public void EndAttack()
    {
        playerController.EndAttack();
    }
}
