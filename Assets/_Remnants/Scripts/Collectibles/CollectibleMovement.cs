
using UnityEngine;

public class CollectibleFloat : MonoBehaviour
{
    #region Float Settings
    [SerializeField] private float floatHeight = 0.15f;
    [SerializeField] private float floatSpeed = 2f;
    #endregion

    #region Private Variables
    private Vector3 startPosition;
    #endregion

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.localPosition = startPosition + Vector3.up * offset;
    }
}
