using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
/**
public class MenuController : MonoBehaviour
{

    public GameObject menuPanel;

	public float fadeDuration = 0.5f;

	private CanvasGroup firstPanelCanvasGroup;

	// Start is called before the first frame update
	void Start()
    {
		firstPanelCanvasGroup = GetComponent<CanvasGroup>();
		menuPanel.SetActive(false);
    }

	void Update()
	{
		if (Input.anyKeyDown)
		{
			StartCoroutine(FadeOutFirstPanel());
		}
	}

	IEnumerator FadeOutFirstPanel()
	{
		float t = 0f;
		while (t < fadeDuration)
		{
			t += Time.deltaTime;
			firstPanelCanvasGroup.alpha = 1f - Mathf.Clamp01(t / fadeDuration);
			yield return null;
		}

		firstPanelCanvasGroup.alpha = 0f;
		firstPanelCanvasGroup.interactable = false;
		firstPanelCanvasGroup.blocksRaycasts = false;

		menuPanel.SetActive(true);
		StartCoroutine(FadeInMenuPanel());
	}

	IEnumerator FadeInMenuPanel()
	{
		CanvasGroup secondPanelCanvasGroup = menuPanel.GetComponent<CanvasGroup>();
		secondPanelCanvasGroup.alpha = 0f;

		float t = 0f;
		while (t < fadeDuration)
		{
			t += Time.deltaTime;
			secondPanelCanvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);
			yield return null;
		}

		secondPanelCanvasGroup.alpha = 1f;
	}
}
**/

public class MenuController : MonoBehaviour
{
	public GameObject secondPanel;
	public TextMeshProUGUI pressAnyButtonText;
	public float fadeDuration = 0.5f;
	public float textFadeDuration = 1f;
	public float idleTimeBeforeFlash = 3f;
	public float textFlashDuration = 0.5f;
	public float minAlpha = 0.2f;
	public float maxAlpha = 0.8f;

	private CanvasGroup firstPanelCanvasGroup;
	private bool isFading = false;
	private bool isIdle = true;
	private bool isFlashing = false;

	void Start()
	{
		firstPanelCanvasGroup = GetComponent<CanvasGroup>();
		secondPanel.SetActive(false);
		StartCoroutine(FadeInPressAnyButtonText());
	}

	void Update()
	{
		if (Input.anyKeyDown && !isFading)
		{
			StopAllCoroutines();
			StartCoroutine(FadeOutPressAnyButtonText());
			StartCoroutine(FadeOutFirstPanel());
		}
	}

	IEnumerator FadeInPressAnyButtonText()
	{
		pressAnyButtonText.color = new Color(pressAnyButtonText.color.r, pressAnyButtonText.color.g, pressAnyButtonText.color.b, 0f);

		float startTime = Time.time;

		while (true)
		{
			while (isIdle)
			{
				float alpha = Mathf.Lerp(maxAlpha, minAlpha, Mathf.PingPong(Time.time - startTime, idleTimeBeforeFlash) / idleTimeBeforeFlash);
				pressAnyButtonText.color = new Color(pressAnyButtonText.color.r, pressAnyButtonText.color.g, pressAnyButtonText.color.b, alpha);
				yield return null;

			}

			while (isFlashing)
			{
				float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.time - startTime, textFlashDuration) / textFlashDuration);
				pressAnyButtonText.color = new Color(pressAnyButtonText.color.r, pressAnyButtonText.color.g, pressAnyButtonText.color.b, alpha);
				yield return null;
			}
		}
	}

	IEnumerator FadeOutPressAnyButtonText()
	{
		isFading = true;

		float t = 0f;
		while (t < textFadeDuration)
		{
			t += Time.deltaTime;
			float alpha = Mathf.Lerp(1f, 0f, Mathf.Clamp01(t / textFadeDuration));
			pressAnyButtonText.color = new Color(pressAnyButtonText.color.r, pressAnyButtonText.color.g, pressAnyButtonText.color.b, alpha);
			yield return null;
		}

		isFading = false;
	}

	IEnumerator FadeOutFirstPanel()
	{
		isFading = true;

		float t = 0f;
		while (t < fadeDuration)
		{
			t += Time.deltaTime;
			firstPanelCanvasGroup.alpha = 1f - Mathf.Clamp01(t / fadeDuration);
			yield return null;
		}

		firstPanelCanvasGroup.alpha = 0f;
		firstPanelCanvasGroup.interactable = false;
		firstPanelCanvasGroup.blocksRaycasts = false;

		secondPanel.SetActive(true);
		StartCoroutine(FadeInSecondPanel());
	}

	IEnumerator FadeInSecondPanel()
	{
		CanvasGroup secondPanelCanvasGroup = secondPanel.GetComponent<CanvasGroup>();
		secondPanelCanvasGroup.alpha = 0f;

		float t = 0f;
		while (t < fadeDuration)
		{
			t += Time.deltaTime;
			secondPanelCanvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);
			yield return null;
		}
		gameObject.SetActive(false);
		secondPanelCanvasGroup.alpha = 1f;
	}
}

