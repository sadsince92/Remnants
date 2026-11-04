
using UnityEngine;

public class DreamShardCollectible : MonoBehaviour
{
    #region Component References
    [SerializeField] private GameObject collectEffectPrefab;

    private DreamShardCounterUI counterUI;
    #endregion

    #region Effect Settings
    [SerializeField] private Vector3 effectOffset = new Vector3(0f, -0.3f, 0f);
    #endregion

    private void Start()
    {
        counterUI = FindAnyObjectByType<DreamShardCounterUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(20);
            }
            if (counterUI != null)
            {
                counterUI.AddDreamShard();
            }

            if (collectEffectPrefab != null)
            {
                GameObject effect = Instantiate(collectEffectPrefab, other.transform);

                effect.transform.localPosition = effectOffset;

            }

            Destroy(gameObject);
        }
    }
}


