using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
	public void PlayButton()
	{
		if (!SaveSystemManager.instance.HasSaveData())
		{
			SaveSystemManager.instance.NewGame();
			SceneManager.LoadSceneAsync("Tutorial");
		}
		else SceneManager.LoadSceneAsync("Hub");
	}

	public void SettingsButton(GameObject settingsArea)
	{
		settingsArea.SetActive(true);
		settingsArea.GetComponent<SettingsManager>().SettingsFadeIn();
	}

	public void QuitButton()
	{
		Application.Quit();
	}
}
