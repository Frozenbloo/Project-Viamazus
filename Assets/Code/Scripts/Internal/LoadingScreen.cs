using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
	[SerializeField] private Image panel;
	[SerializeField] private GameObject uiPrefab;
	[SerializeField] private float fadeDuration = 1.0f;

	private void Start()
	{
		panel.gameObject.SetActive(false);
	}

	public void LoadSceneAsync(string sceneName)
	{
		StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
	}

	private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
	{
		panel.gameObject.SetActive(true);
		// Fade in
		float timer = 0;
		Color panelColor = panel.color;
		while (timer < fadeDuration)
		{
			timer += Time.deltaTime;
			uiPrefab.SetActive(false);
			panelColor.a = Mathf.Lerp(0, 1, timer / fadeDuration);
			panel.color = panelColor;
			yield return null;
		}

		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
		asyncOperation.allowSceneActivation = false;

		while (!asyncOperation.isDone)
		{
			if (asyncOperation.progress >= 0.9f)
			{
				asyncOperation.allowSceneActivation = true;
			}
			yield return null;
		}

		// Fade out
		timer = 0;
		while (timer < fadeDuration)
		{
			timer += Time.deltaTime;
			panelColor.a = Mathf.Lerp(1, 0, timer / fadeDuration);
			panel.color = panelColor;
			uiPrefab.SetActive(!asyncOperation.isDone);
			yield return null;
		}
	}

}
