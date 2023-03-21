using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public ConfirmationManager confirmationManager;

	private string pName = "The Maze's Whisper:";

	protected override void Start()
	{
		confirmationManager = FindObjectOfType<ConfirmationManager>();
		if (confirmationManager != null) Debug.Log("ConfirmationManager Found");
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
		ButtonActions = new Action[] { () => 
		{ 
			Debug.Log("Exit confirmed");
			
		}, () => { Debug.Log("Exit canceled"); } };

		confirmationManager.StartConfirmation(this);
	}
	public void OnConfirmationEnd()
	{
		Debug.Log("Confirmation ended");
	}

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact"))
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this, gameObject);
		}
		else base.OnCollide(collider);
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
