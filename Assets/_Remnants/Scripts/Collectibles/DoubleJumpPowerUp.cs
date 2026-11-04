using UnityEngine;

public class DoubleJumpPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject collectFXPrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.UnlockDoubleJump();
            GameObject effect = Instantiate(collectFXPrefab, other.transform);
            effect.transform.localPosition = new Vector3(0f, -0.3f, 0f);
            Destroy(gameObject);
        }
    }
}