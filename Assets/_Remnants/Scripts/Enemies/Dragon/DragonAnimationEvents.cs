
using UnityEngine;

public class DragonAnimationEvents : MonoBehaviour
{
    private DragonController dragonController;

    private void Awake()
    {
        dragonController = GetComponentInParent<DragonController>();
    }

    public void SpawnFireball()
    {
        if (dragonController != null)
        {
            dragonController.SpawnFireball();
        }
    }
}
