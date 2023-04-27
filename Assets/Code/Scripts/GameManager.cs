using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, ISave
{
	//needs to be accessed everywhere
	[SerializeField] public static GameManager instance { get; private set; }
	[SerializeField] private Player player;
	private Image healthBar;
	public int playerLvl;

	private void Awake()
	{
		if (GameManager.instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);

		healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
	}

	private void Update()
	{
		healthBar.fillAmount = player.HP / player.maxHP;
	}

	public void LoadData(GameSave data)
	{
		playerLvl = data.playerLvl;
	}

	public void SaveData(ref GameSave data)
	{
		data.playerLvl = playerLvl;
	}
}