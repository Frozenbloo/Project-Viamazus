using TMPro;
using UnityEngine;

public class GoldAmountText : MonoBehaviour, ISave
{
	private int goldAmount;
	private TextMeshProUGUI goldText;

	private void Awake()
	{
		goldText = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		// TODO goldCollectEvent
	}

	private void Update()
    {
        goldText.text = goldAmount.ToString();
    }

	private void OnDestroy()
	{
	
	}

	private void OnGoldCollect(int amount)
	{
		goldAmount += amount;
	}

	public void LoadData(GameSave data)
	{
		this.goldAmount = data.goldCount;
	}

	public void SaveData(ref GameSave data)
	{
		data.goldCount = this.goldAmount;
	}
}
