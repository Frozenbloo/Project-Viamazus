using UnityEngine;

public class WizardNPC : Interactable, IDialogue
{
	private string[] pDialogue =
	{
		"Howdy Partner!",
		"This is a test for dialogue!",
		"I want to see how long i can make this dialogue, although it will probably be very long lorum ipsum etc etc"
	};

	private string pNpcName = "Wizard:";

	public string npcName { get => pNpcName; set => pNpcName = value; }

	public string[] dialogue { get => pDialogue; set => pDialogue = value; }

	public void OnDialogueEnd() {}

	protected override void OnCollide(Collider2D collider)	{
		if (Input.GetButtonDown("Interact"))
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this, gameObject);
		}
		else base.OnCollide(collider);
	}

}
