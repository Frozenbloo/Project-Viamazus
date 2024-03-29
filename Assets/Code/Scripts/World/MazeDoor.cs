using System;
using UnityEngine;

public class MazeDoor : Interactable, IDialogue, IConfirmation
{
	private string[] pDialogue =
	{
		"Enter the maze if you dare, for its path is ever-changing and its secrets elusive.",
		"The maze is a fickle beast that delights in toying with its prey.",
		"Will you be able to decipher its mysteries before it swallows you whole?"
	};

	public string Message { get; set; }
	public string[] ButtonTexts { get; set; }
	public Action[] ButtonActions { get; set; }

	private ConfirmationManager confirmationManager;

	private string pName = "The Maze's Whisper:";

	protected override void Start()
	{
		confirmationManager = FindObjectOfType<ConfirmationManager>();
		base.Start();
	}

	public string[] dialogue { get => pDialogue; set => pDialogue = value; }
	public string npcName { get => pName; set => pName = value; }

	public void OnDialogueEnd()
	{
		ShowConfirmation();
	}

	public void ShowConfirmation()
	{
		Message = "Are you sure you want to enter the maze?";
		ButtonTexts = new string[] { "Yes", "No" };
		ButtonActions = new Action[] { () => { GameObject.FindObjectOfType<LoadingScreen>().LoadSceneAsync("MazeWorld"); }, () => { } };

		confirmationManager.StartConfirmation(this);
	}
	public void OnConfirmationEnd() { }

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact"))
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this, gameObject);
		}
		else base.OnCollide(collider);
	}
}
