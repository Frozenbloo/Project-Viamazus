using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPrincessNPC : Interactable, IDialogue
{
	private string[] pDialogue =
	{
		"I'm a princess!"
	};

	private string pNpcName = "Princess:";

	public string[] dialogue { get => pDialogue; set => pDialogue = value; }
	public string npcName { get => pNpcName; set => pNpcName = value; }

	public void OnDialogueEnd()
	{

	}

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact"))
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this, gameObject);
		}
		else base.OnCollide(collider);
	}
}
