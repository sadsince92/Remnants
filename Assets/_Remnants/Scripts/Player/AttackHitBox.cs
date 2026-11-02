using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    #region Attack 1 Settings
    [SerializeField] private int attack1Damage = 20;
    [SerializeField] private Vector2 attack1Size = new Vector2(1f, 1.2f);
    [SerializeField] private Vector2 attack1Offset = new Vector2(0f, 0.2f);
    #endregion

    #region Attack 2 Settings
    [SerializeField] private int attack2Damage = 30;
    [SerializeField] private Vector2 attack2Size = new Vector2(1.5f, 0.8f);
    [SerializeField] private Vector2 attack2Offset = new Vector2(0f, -0.1f);
    #endregion

    #region Current Attack
    private int attackDamage = 20;
    #endregion

    #region Component References
    private BoxCollider2D hitboxCollider;
    #endregion

    private HashSet<EnemyHealth> enemiesHit = new HashSet<EnemyHealth>();

    private void Awake()
    {
        hitboxCollider = GetComponent<BoxCollider2D>();
        hitboxCollider.enabled = false;
    }

    public void ConfigureAttack(int attackNumber)
    {
        if (attackNumber == 1)
        {
            attackDamage = attack1Damage;
            hitboxCollider.size = attack1Size;
            hitboxCollider.offset = attack1Offset;
        }
        else if (attackNumber == 2)
        {
            attackDamage = attack2Damage;
            hitboxCollider.size = attack2Size;
            hitboxCollider.offset = attack2Offset;
        }
    }

    public void EnableHitbox()
    {
        enemiesHit.Clear();
        hitboxCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        hitboxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();

        if (enemy == null || enemiesHit.Contains(enemy))
            return;

        enemiesHit.Add(enemy);
        enemy.TakeDamage(attackDamage);
    }
}

