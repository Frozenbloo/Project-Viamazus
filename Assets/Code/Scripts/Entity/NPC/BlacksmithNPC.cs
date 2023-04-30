using System;
using UnityEngine;

public class BlacksmithNPC : Interactable, IDialogue, IConfirmation
{
	private string[] pDialogue =
	{
		"Wow, that sword is terrible.",
		"Where did you even get that?",
		"What do you mean it was free?",
		"Look, I can sharpen that for you, but it won’t be free."
	};

	private string pNpcName = "Blacksmith:";
	private ConfirmationManager confirmationManager;

	public string npcName { get => pNpcName; set => pNpcName = value; }

	public string[] dialogue { get => pDialogue; set => pDialogue = value; }
	public string Message { get; set; }
	public string[] ButtonTexts { get; set; }
	public Action[] ButtonActions { get; set; }

	public void OnConfirmationEnd() { }

	public void OnDialogueEnd() { ShowConfirmation(); }

	public void ShowConfirmation()
	{
		Message = "Buy Damage Upgrade? " + GameManager.instance.GetWeaponDmg() + " -> " + (GameManager.instance.GetWeaponDmg() + 1);
		ButtonTexts = new string[] { "-100 gold", "No" };
		ButtonActions = new Action[] { () => { BuyItem(100); }, () => { } };

		confirmationManager.StartConfirmation(this);
	}

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact"))
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this, gameObject);
		}
		else base.OnCollide(collider);
	}

	protected override void Start()
	{
		confirmationManager = FindObjectOfType<ConfirmationManager>();
		base.Start();
	}

	private void BuyItem(int cost)
	{
		if (GameManager.instance.GetPlayerGold() >= cost)
		{
			GameManager.instance.UpdatePlayerGold(-cost);
			GameManager.instance.UpgradeWeaponDmg();
		}
	}
}
