using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void PlayButton()
    {
		SceneManager.LoadScene("Hub");
	}

    public void SettingsButton()
    {

    }

	public void QuitButton()
	{
		Application.Quit();
	}
}
