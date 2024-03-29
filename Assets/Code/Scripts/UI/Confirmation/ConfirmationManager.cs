using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationManager : MonoBehaviour
{
	[SerializeField] private GameObject confirmationScreen;
	[SerializeField] private TextMeshProUGUI messageText;
	[SerializeField] private Button[] buttons;

	private IConfirmation confirmation;

	private void Start()
	{
		NullConfirmation();
	}

	private void Update()
	{
		if (confirmation != null)
		{
			// Check for input to close the confirmation screen if necessary.
		}
	}

	public void StartConfirmation(IConfirmation confirmation)
	{
		this.confirmation = confirmation;
		messageText.text = confirmation.Message;

		for (int i = 0; i < buttons.Length; i++)
		{
			if (i < confirmation.ButtonTexts.Length)
			{
				buttons[i].gameObject.SetActive(true);
				TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
				buttonText.text = confirmation.ButtonTexts[i];
				buttons[i].onClick.RemoveAllListeners();
				int index = i;
				buttons[i].onClick.AddListener(() =>
				{
					if (index < confirmation.ButtonActions.Length)
					{
						confirmation.ButtonActions[index]?.Invoke();
					}
					EndConfirmation();
				});
			}
			else
			{
				buttons[i].gameObject.SetActive(false);
			}
		}

		confirmationScreen.SetActive(true);
	}

	private void EndConfirmation()
	{
		confirmationScreen.SetActive(false);
		confirmation.OnConfirmationEnd();
		NullConfirmation();
	}

	private void NullConfirmation()
	{
		confirmationScreen.SetActive(false);
		confirmation = null;
		messageText.text = string.Empty;
	}
}
