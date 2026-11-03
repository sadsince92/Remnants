using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFade : MonoBehaviour
{
    #region Fade Settings
    [SerializeField] private float fadeDuration = 0.4f;
    #endregion

    #region Component References
    private Light2D light2D;
    private float startIntensity;
    private float fadeTimer;
    #endregion

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        startIntensity = light2D.intensity;
    }

    private void Update()
    {
        fadeTimer += Time.deltaTime;

        float progress = Mathf.Clamp01(fadeTimer / fadeDuration);

        light2D.intensity = Mathf.Lerp(startIntensity, 0f, progress);
    }
}

