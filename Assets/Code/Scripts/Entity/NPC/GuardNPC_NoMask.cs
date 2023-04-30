using UnityEngine;

public class GuardNPC_NoMask : Interactable, IDialogue
{
	private string[] pDialogue =
	{
		"Hey, can you tell the other guard to open his mask?",
		"I never understand a thing he says, but it’s been too long for me to say something now…"
	};

	private string pNpcName = "Guard";

	public string npcName { get => pNpcName; set => pNpcName = value; }
	public string[] dialogue { get => pDialogue; set => pDialogue = value; }

	public void OnDialogueEnd() { }

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact"))
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this, this.gameObject);
		}
		else base.OnCollide(collider);
	}
}
