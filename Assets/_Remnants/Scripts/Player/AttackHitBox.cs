
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    #region Component References
    private BoxCollider2D hitboxCollider;
    #endregion
    private void Awake()
    {
        hitboxCollider = GetComponent<BoxCollider2D>();
        hitboxCollider.enabled = false;
    }

    public void EnableHitbox()
    {
        hitboxCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        hitboxCollider.enabled = false;
    }
}
