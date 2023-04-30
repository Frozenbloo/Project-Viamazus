using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	//needs to be accessed everywhere
	[SerializeField] public static GameManager instance { get; private set; }
	[SerializeField] private Player player;

	private void Awake()
	{
		if (GameManager.instance != null)
		{
			Destroy(gameObject);
			return;
		}
		player = GameObject.Find("Player").GetComponent<Player>();
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
	}

	private void Start()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
	}

	public int GetPlayerLevel()
	{
		return player.getLevel();
	}

	public int GetPlayerGold()
	{
		return player.getGold();
	}

	public void UpdatePlayerGold(int amount)
	{
		player.updateGold(amount);
	}

	public float GetPlayerMaxHealth()
	{
		return player.getMaxHealth();
	}

	public float GetWeaponDmg()
	{
		return player.getWeaponDmg();
	}

	public void UpgradePlayerMaxHealth()
	{
		player.upgradeMaxHealth();
	}

	public void UpgradeWeaponDmg()
	{
		player.upgradeWeaponDmg();
	}
}