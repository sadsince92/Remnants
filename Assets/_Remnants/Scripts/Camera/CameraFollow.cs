using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Tunable Parameters
    [SerializeField] private Transform target;
    [SerializeField] private float verticalDeadZone = 2f;
    [SerializeField] private float verticalSmoothTime = 0.3f;
    [SerializeField] private float fallSmoothTime = 0.1f;

    [Header("Camera Boundaries")]
    [SerializeField] private float minX = -20f;
    [SerializeField] private float maxX = 20f;
    [SerializeField] private float minY = -100f;
    [SerializeField] private float maxY = 10f;
    #endregion

    #region Private Variables
    private float velocityY;
    private Camera cam;
    #endregion

    void LateUpdate()
    {
        if (target == null) return;

        // Horizontal following with boundaries
        float cameraHalfWidth = cam.orthographicSize * cam.aspect;

        float newX = Mathf.Clamp(target.position.x, minX + cameraHalfWidth, maxX - cameraHalfWidth);

        // Vertical following with dead zone
        float newY = transform.position.y;
        float verticalDistance = target.position.y - transform.position.y;

        if (Mathf.Abs(verticalDistance) > verticalDeadZone)
        {
            float targetY = target.position.y - Mathf.Sign(verticalDistance) * verticalDeadZone;
            float smoothTime = verticalDistance < 0 ? fallSmoothTime : verticalSmoothTime;

            newY = Mathf.SmoothDamp(transform.position.y, targetY, ref velocityY, smoothTime);
        }
        else
        {
            velocityY = 0f;
        }

        // Vertical camera boundaries
        float cameraHalfHeight = cam.orthographicSize;
        newY = Mathf.Clamp(newY, minY + cameraHalfHeight, maxY - cameraHalfHeight);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
    void Awake()
    {
        cam = GetComponent<Camera>();
    }
}