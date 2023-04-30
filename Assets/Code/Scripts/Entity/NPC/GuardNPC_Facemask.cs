using UnityEngine;

public class GuardNPC_Facemask : Interactable, IDialogue
{
	private string[] pDialogue =
	{
		"*muffled mumbling*"
	};

	private string pNpcName = "Guard";

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
