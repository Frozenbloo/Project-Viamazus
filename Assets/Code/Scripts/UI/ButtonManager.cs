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
		SceneManager.LoadScene("Hub");
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
