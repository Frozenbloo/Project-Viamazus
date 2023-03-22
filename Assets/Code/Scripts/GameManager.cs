using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //needs to be accessed everywhere
    public static GameManager instance;

	public TextMeshProUGUI goldText;

	private void Awake()
	{
		if (GameManager.instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		player.setExp(playerExp);
		//Adds the saveState function to the sceneLoaded event
		SceneManager.sceneLoaded += LoadState;
		SetUIValues();
		DontDestroyOnLoad(gameObject);
	}

	private void SetUIValues()
	{
		goldText.text = player.getGold().ToString();
	}

	#region GamePlay
	private int playerExp;
	#endregion

	//Player Related Things that need to be saved
	public Player player;
	private int gold, exp, mazesBeat, mazeRuns, weaponUpgrade;

	public int Gold { get => gold; set => gold = value; }
	public int Exp { get => exp; set => exp = value; }
	public int MazesBeat { get => mazesBeat; set => mazesBeat = value; }
	public int MazeRuns { get => mazeRuns; set => mazeRuns = value; }
	public int WeaponUpgrade { get => weaponUpgrade; set => weaponUpgrade = value; }

	#region SaveGame
	//Saving GameState
	public void SaveState()
	{
		string gameSave = "";

		gameSave += gold.ToString() + "|";
		gameSave += exp.ToString() + "|";
		gameSave += weaponUpgrade.ToString() + "|";
		gameSave += mazesBeat.ToString() + "|";
		gameSave += mazeRuns.ToString();

		PlayerPrefs.SetString("SaveGame", gameSave);
	}

	public void LoadState(Scene scene, LoadSceneMode loadSceneMode) 
	{
		Debug.Log("Attemping To Load from Save Game");
		if (PlayerPrefs.HasKey("SaveGame"))
		{
			string[] saveData = PlayerPrefs.GetString("SaveGame").Split('|');

			gold = int.Parse(saveData[1]);
			exp = int.Parse(saveData[2]);
			weaponUpgrade = int.Parse(saveData[3]);
			mazesBeat = int.Parse(saveData[4]);
			mazeRuns = int.Parse(saveData[5]);

			Debug.Log("Game Loaded Succesfully from file");
		}
		else
		{
			Debug.Log("Save file doesn't exist, loading defaults");
			//Theres no point on saving the game if the player doesn't even leave the hub as they gain nothing
		}
	}
	#endregion
}
