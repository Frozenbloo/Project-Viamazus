using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
	[SerializeField] private float fadeDuration = 1f;

	private Image fadeImage;

	private void Start()
	{
		fadeImage = GetComponentInChildren<Image>();
		fadeImage.gameObject.SetActive(true);
		StartCoroutine(FadeIn());
	}

	private IEnumerator FadeIn()
	{
		fadeImage.gameObject.SetActive(true);

		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime / fadeDuration;
			Color color = fadeImage.color;
			color.a = t;
			fadeImage.color = color;
			yield return null;
		}

		fadeImage.gameObject.SetActive(false);
	}
}
