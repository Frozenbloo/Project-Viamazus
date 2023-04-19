using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
	[SerializeField] float fadeDuration;

	[Header("Master Volume")]
	[SerializeField] AudioMixer masterAudioMixer;
	[SerializeField] TextMeshProUGUI masterSliderValue;
	[SerializeField] Slider masterSlider;

	[Header("Music Volume")]
	[SerializeField] AudioMixer musicAudioMixer;
	[SerializeField] TextMeshProUGUI musicSliderValue;
	[SerializeField] Slider musicSlider;

	[Header("SFX Volume")]
	[SerializeField] AudioMixer sfxAudioMixer;
	[SerializeField] TextMeshProUGUI sfxSliderValue;
	[SerializeField] Slider sfxSlider;

	[Header("UI Volume")]
	[SerializeField] AudioMixer uiAudioMixer;
	[SerializeField] TextMeshProUGUI uiSliderValue;
	[SerializeField] Slider uiSlider;

	private bool settingsOn = false;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SettingsFadeIn()
	{
		gameObject.SetActive(true);
		if (!settingsOn)
		{
			StartCoroutine(FadeInSettings());
		}
		else
		{
			StartCoroutine(FadeOutSettings());
		}
	}

	IEnumerator FadeInSettings()
	{
		settingsOn = true;
		CanvasGroup settingsPanelCanvasGroup = this.GetComponent<CanvasGroup>();
		settingsPanelCanvasGroup.alpha = 0f;

		float t = 0f;
		while (t <= fadeDuration)
		{
			t += Time.deltaTime;
			settingsPanelCanvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);
			yield return null;
		}
		settingsPanelCanvasGroup.alpha = 1f;
	}

	IEnumerator FadeOutSettings()
	{
		settingsOn = false;
		CanvasGroup settingsPanelCanvasGroup = this.GetComponent<CanvasGroup>();
		settingsPanelCanvasGroup.alpha = 1f;

		float t = 1f;
		while (t <= fadeDuration)
		{
			t -= Time.deltaTime;
			settingsPanelCanvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);
			yield return null;
			if (t <= 0) break;
		}
		settingsPanelCanvasGroup.alpha = 0f;
		gameObject.SetActive(false);
	}

	public void SetMasterVolume()
	{
		masterSliderValue.text = Mathf.RoundToInt(masterSlider.value + 80).ToString() + "%";

		masterAudioMixer.SetFloat("Master", masterSlider.value);
	}

	public void SetMusicVolume()
	{
		musicSliderValue.text = Mathf.RoundToInt(musicSlider.value + 80).ToString() + "%";

		musicAudioMixer.SetFloat("Music", musicSlider.value);
	}

	public void SetSFXVolume()
	{
		sfxSliderValue.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString() + "%";

		sfxAudioMixer.SetFloat("SFX", sfxSlider.value);
	}

	public void SetUIVolume()
	{
		uiSliderValue.text = Mathf.RoundToInt(uiSlider.value + 80).ToString() + "%";

		uiAudioMixer.SetFloat("UI", uiSlider.value);
	}
}
