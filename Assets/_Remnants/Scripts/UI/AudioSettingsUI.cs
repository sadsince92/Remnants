
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    #region Audio References
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    #endregion

    #region Volume Settings
    private const float minVolume = 0.0001f;
    #endregion

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        soundSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundSlider.onValueChanged.AddListener(SetSoundVolume);

        SetMusicVolume(musicSlider.value);
        SetSoundVolume(soundSlider.value);
    }

    public void SetMusicVolume(float volume)
    {
        float decibels = Mathf.Log10(Mathf.Max(volume, minVolume)) * 20f;

        audioMixer.SetFloat("musicVolume", decibels);

        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSoundVolume(float volume)
    {
        float decibels = Mathf.Log10(Mathf.Max(volume, minVolume)) * 20f;

        audioMixer.SetFloat("sfxVolume", decibels);

        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();
    }
}
