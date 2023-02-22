using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardNPC : Interactable, IDialogue
{
	private string[] dialogue =
	{
		"Howdy Partner!",
		"This is a test for dialogue!",
		"I want to see how long i can make this dialogue, although it will probably be very long lorum ipsum etc etc"
	};

	string[] IDialogue.dialogue { get => dialogue; set => dialogue = value; }

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact"))
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this);
		}
		else base.OnCollide(collider);
	}
}
