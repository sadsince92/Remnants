using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    public void EndHurt()
    {
        playerHealth.EndHurt();
    }
}
