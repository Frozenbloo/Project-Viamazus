using UnityEngine;

public class NoEntryInteract : Interactable, IDialogue
{
	private string[] pDialogue =
	{
		"Hold on a second there, buddy. You can't go that way.",
		"That's the internet, and it's not safe for RPG characters like us.",
		"The last time someone went in there, they got sucked into a browser game and couldn't escape for weeks.",
		"And let's not even talk about the pop-up ads."
	};

	private string pNpcName = "???";

	public string npcName { get => pNpcName; set => pNpcName = value; }
	public string[] dialogue { get => pDialogue; set => pDialogue = value; }

	public void OnDialogueEnd() { }

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact"))
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this, gameObject);
		}
		else base.OnCollide(collider);
	}

}
