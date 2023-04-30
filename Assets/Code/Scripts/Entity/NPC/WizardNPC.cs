using System;
using UnityEngine;

public class WizardNPC : Interactable, IDialogue, IConfirmation
{
	private string[] pDialogue =
	{
		"You’re going in there?",
		"…You look pretty weak, are you sure you’ll be okay?",
		"I can give you some potions to improve your health if you need them.",
		"For money of course."
	};

	private string pNpcName = "Wizard:";
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
		Message = "Buy Health Upgrade? " + GameManager.instance.GetPlayerMaxHealth() + " -> " + (GameManager.instance.GetPlayerMaxHealth() + 1);
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
			GameManager.instance.UpgradePlayerMaxHealth();
		}
	}
}
