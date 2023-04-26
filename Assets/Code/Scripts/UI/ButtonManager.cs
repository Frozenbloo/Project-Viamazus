using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
