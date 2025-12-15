using UnityEngine;
using UnityEngine.UI;

public class SettingVolume : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;

    [Header("Audio Sources")]
    public AudioSource[] musicSources;
    public AudioSource[] sfxSources;

    [Header("Player Settings")]
    public Movement playerController;

    public GameObject settingsCanvas;

    public GameObject mainMenuCanvas;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 1f);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetSensitivity(sensitivitySlider.value);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);

        foreach (var hover in settingsCanvas.GetComponentsInChildren<HoverEffect>(true))
        {
            hover.ResetColor();
        }
    }

    public void SetMusicVolume(float value)
    {
        foreach (AudioSource src in musicSources)
            if (src != null) src.volume = value;

        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        foreach (AudioSource src in sfxSources)
            if (src != null) src.volume = value;

        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetSensitivity(float value)
    {
        if (playerController != null)
            playerController.lookSpeed = value;

        PlayerPrefs.SetFloat("Sensitivity", value);
    }

    public void CloseSettings()
    {
        foreach (var hover in settingsCanvas.GetComponentsInChildren<HoverEffect>(true))
        {
            hover.ResetColor();
        }

        //PlayerPrefs.Save();

        settingsCanvas.SetActive(false);

        ResumeESC pauseManager = FindObjectOfType<ResumeESC>();

        if (pauseManager != null)
        {
            pauseManager.OpenPauseMenu();
        }

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
        }
    }
}