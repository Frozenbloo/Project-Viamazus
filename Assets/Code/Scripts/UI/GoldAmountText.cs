using System.Collections;
using TMPro;
using UnityEngine;

public class GoldAmountText : MonoBehaviour, ISave
{
	private int goldAmount;
	private TextMeshProUGUI goldText;
	private GameManager gameManager;

	private void Awake()
	{
		goldText = GetComponent<TextMeshProUGUI>();
		StartCoroutine(getGameManager());
		gameManager = GameManager.instance;
		goldAmount = gameManager.GetPlayerGold();
		if (GameEvents.instance.onCoinCollect == null) GameEvents.instance.onCoinCollect = new ViamazusIntEvent();
		GameEvents.instance.onCoinCollect.AddListener(OnGoldCollect);
	}

	private void Update()
    {
		if (goldText.text == "%GOLD%") StartCoroutine(getGameManager());
		goldAmount = gameManager.GetPlayerGold();
        goldText.text = goldAmount.ToString();
    }

	private void OnDestroy()
	{
		GameEvents.instance.onCoinCollect.RemoveListener(OnGoldCollect);
	}

	private void OnGoldCollect(int amount)
	{
		gameManager.UpdatePlayerGold(amount);
	}

	public void LoadData(GameSave data)
	{
		goldAmount = data.goldCount;
	}

	public void SaveData(ref GameSave data)
	{
	}

	IEnumerator getGameManager()
	{
		yield return new WaitForSeconds(1);
		gameManager = GameManager.instance;
	}
}
