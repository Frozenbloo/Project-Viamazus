using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextColourManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] TextMeshProUGUI buttonText;
	[SerializeField] Color hoverTextColor;
	[SerializeField] float fadeDuration = 1f;

	private Color defaultTextColor;
	private Coroutine fadeCoroutine;

	void Start()
    {
        defaultTextColor = buttonText.color;
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
		}
		fadeCoroutine = StartCoroutine(FadeToColor(hoverTextColor));
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
		}
		fadeCoroutine = StartCoroutine(FadeToColor(defaultTextColor));
	}

	IEnumerator FadeToColor(Color targetColor)
	{
		float startTime = Time.time;
		Color initialColor = buttonText.color;

		while (Time.time < startTime + fadeDuration)
		{
			float t = (Time.time - startTime) / fadeDuration;
			Color newColor = Color.Lerp(initialColor, targetColor, t);
			newColor.a = buttonText.color.a; // Preserve the original alpha value
			buttonText.color = newColor;
			yield return null;
		}

		targetColor.a = buttonText.color.a; // Preserve the original alpha value
		buttonText.color = targetColor;
	}
}
