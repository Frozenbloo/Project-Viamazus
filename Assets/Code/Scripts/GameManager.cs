using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	//needs to be accessed everywhere
	[SerializeField] static GameManager instance;
	private Image healthBar;


	private void Awake()
	{
		if (GameManager.instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		player.setExp(playerExp);

		healthBar = GameObject.Find("HealthBar").GetComponent<Image>();

		DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{
		healthBar.fillAmount = player.HP / player.maxHP;
	}


	#region GamePlay
	private int playerExp;
	#endregion

	//Player Related Things that need to be saved
	public Player player;
	private int exp, mazesBeat, mazeRuns, weaponUpgrade;

	public int Exp { get => exp; set => exp = value; }
	public int MazesBeat { get => mazesBeat; set => mazesBeat = value; }
	public int MazeRuns { get => mazeRuns; set => mazeRuns = value; }
	public int WeaponUpgrade { get => weaponUpgrade; set => weaponUpgrade = value; }
}