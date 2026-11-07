
using UnityEngine;

public class DragonController : MonoBehaviour
{
    #region Detection Settings
    [SerializeField] private float detectionRange = 8f;

    private Transform player;
    #endregion

    #region Component References
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballPrefab;
    #endregion

    #region Attack Settings
    [SerializeField] private float attackCooldown = 2f;
    private float attackTimer;
    #endregion

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }

        if (distanceToPlayer <= detectionRange)
        {
            FacePlayer();

            if (attackTimer <= 0f)
            {
                animator.SetTrigger("attack");
                attackTimer = attackCooldown;
            }
        }
    }


    private void FacePlayer()
    {
        if (player.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }


    public void SpawnFireball()
    {
        if (fireballPrefab == null || firePoint == null)
        {
            return;
        }

        Vector3 spawnPosition = firePoint.position;

        float mouthOffsetX = Mathf.Abs(firePoint.localPosition.x);

        if (spriteRenderer.flipX)
        {
            spawnPosition.x = transform.position.x + mouthOffsetX;
        }
        else
        {
            spawnPosition.x = transform.position.x - mouthOffsetX;
        }

        Vector2 direction = player.position - spawnPosition;

        GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);

        DragonFireball projectile = fireball.GetComponent<DragonFireball>();

        if (projectile != null)
        {
            projectile.Initialize(direction);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

