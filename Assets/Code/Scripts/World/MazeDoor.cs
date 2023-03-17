using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoor : Interactable, IDialogue
{
	private string[] pDialogue =
	{
		"Enter the maze if you dare, for its path is ever-changing and its secrets elusive.",
		"The maze is a fickle beast that delights in toying with its prey.",
		"Will you be able to decipher its mysteries before it swallows you whole?"
	};

	private string pName = "The Maze's Whisper:";


	public string[] dialogue { get => pDialogue; set => pDialogue = value; }
	public string npcName { get => pName; set => pName = value; }

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
