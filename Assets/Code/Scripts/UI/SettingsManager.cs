using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour, ISave
{
	[SerializeField] private float fadeDuration;

	[Header("Master Volume")]
	[SerializeField] private AudioMixer masterAudioMixer;
	[SerializeField] private TextMeshProUGUI masterSliderValue;
	[SerializeField] private Slider masterSlider;
	private float savedMasterValue;

	[Header("Music Volume")]
	[SerializeField] private AudioMixer musicAudioMixer;
	[SerializeField] private TextMeshProUGUI musicSliderValue;
	[SerializeField] private Slider musicSlider;
	private float savedMusicValue;

	[Header("SFX Volume")]
	[SerializeField] private AudioMixer sfxAudioMixer;
	[SerializeField] private TextMeshProUGUI sfxSliderValue;
	[SerializeField] private Slider sfxSlider;
	private float savedSFXValue;

	[Header("UI Volume")]
	[SerializeField] private AudioMixer uiAudioMixer;
	[SerializeField] private TextMeshProUGUI uiSliderValue;
	[SerializeField] private Slider uiSlider;
	private float savedUIValue;

	private bool settingsOn = false;

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

	private IEnumerator FadeInSettings()
	{
		settingsOn = true;
		CanvasGroup settingsPanelCanvasGroup = GetComponent<CanvasGroup>();
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

	private IEnumerator FadeOutSettings()
	{
		settingsOn = false;
		CanvasGroup settingsPanelCanvasGroup = GetComponent<CanvasGroup>();
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
		masterAudioMixer.SetFloat("Master", masterSlider.value);
	}

	public void SetMusicVolume()
	{
		musicAudioMixer.SetFloat("Music", musicSlider.value);
	}

	public void SetSFXVolume()
	{
		sfxAudioMixer.SetFloat("SFX", sfxSlider.value);
	}

	public void SetUIVolume()
	{
		uiAudioMixer.SetFloat("UI", uiSlider.value);
	}

	private void Update()
	{
		masterSliderValue.text = Mathf.RoundToInt(masterSlider.value + 80).ToString() + "%";
		musicSliderValue.text = Mathf.RoundToInt(musicSlider.value + 80).ToString() + "%";
		sfxSliderValue.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString() + "%";
		uiSliderValue.text = Mathf.RoundToInt(uiSlider.value + 80).ToString() + "%";
	}

	private void Start()
	{
		masterSlider.value = savedMasterValue;
		musicSlider.value = savedMusicValue;
		sfxSlider.value = savedSFXValue;
		uiSlider.value = savedUIValue;
	}

	public void LoadData(GameSave data)
	{
		savedMasterValue = data.masterVolume;
		savedMusicValue = data.musicVolume;
		savedSFXValue = data.SFXVolume;
		savedUIValue = data.UIVolume;
	}

	public void SaveData(ref GameSave data)
	{
		data.masterVolume = masterSlider.value;
		data.musicVolume = musicSlider.value;
		data.SFXVolume = sfxSlider.value;
		data.UIVolume = uiSlider.value;
	}
}
