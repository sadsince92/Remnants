using UnityEngine;

public class TentacleHazard : MonoBehaviour
{
    #region Damage Settings
    [SerializeField] private int contactDamage = 20;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
        }
    }
}

