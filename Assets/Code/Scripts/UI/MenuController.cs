using System.Collections;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	[SerializeField] private GameObject secondPanel;
	[SerializeField] private TextMeshProUGUI pressAnyButtonText;
	[SerializeField] private float fadeDuration = 0.5f;
	[SerializeField] private float textFadeDuration = 1f;
	[SerializeField] private float idleTimeBeforeFlash = 3f;
	[SerializeField] private float textFlashDuration = 0.5f;
	[SerializeField] private float minAlpha = 0.2f;
	[SerializeField] private float maxAlpha = 0.8f;
	[SerializeField] private TextMeshProUGUI playButtonText;

	private CanvasGroup firstPanelCanvasGroup;
	private bool isFading = false;
	private readonly bool isIdle = true;
	private readonly bool isFlashing = false;

	private void Start()
	{
		firstPanelCanvasGroup = GetComponent<CanvasGroup>();
		secondPanel.SetActive(false);
		StartCoroutine(FadeInPressAnyButtonText());
	}

	private void Update()
	{
		if (Input.anyKeyDown && !isFading)
		{
			StopAllCoroutines();
			StartCoroutine(FadeOutPressAnyButtonText());
			StartCoroutine(FadeOutFirstPanel());
		}

		if (!SaveSystemManager.instance.HasSaveData())
		{
			playButtonText.text = "NEW GAME";
		}
		else
		{
			playButtonText.text = "CONTINUE";
		}
	}

	private IEnumerator FadeInPressAnyButtonText()
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

	private IEnumerator FadeOutPressAnyButtonText()
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

	private IEnumerator FadeOutFirstPanel()
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

	private IEnumerator FadeInSecondPanel()
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

