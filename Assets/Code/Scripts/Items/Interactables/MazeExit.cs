using System;
using UnityEngine;

public class MazeExit : Interactable, IConfirmation
{
	[SerializeField] private SpriteRenderer exitSpriteRenderer;
	[SerializeField] private Sprite openDoor;

	private ConfirmationManager confirmationManager;

	private void ExitTheMaze()
	{
		exitSpriteRenderer.sprite = openDoor;
		GameEvents.instance.onMazeBeat.Invoke(0);
		SaveSystemManager.instance.SaveGame();
		GameObject.FindObjectOfType<LoadingScreen>().LoadSceneAsync("Hub");
	}

	protected override void OnCollide(Collider2D collider)
	{
		if (Input.GetButtonDown("Interact") && collider.CompareTag("Player"))
		{
			ShowConfirmation();
		}
		else base.OnCollide(collider);
	}

	protected override void Start()
	{
		confirmationManager = FindObjectOfType<ConfirmationManager>();
		base.Start();
	}

	#region Interfaces
	public string Message { get; set; }
	public string[] ButtonTexts { get; set; }
	public Action[] ButtonActions { get; set; }


	public void OnConfirmationEnd() { }

	public void ShowConfirmation()
	{
		Message = "Are you sure you want to exit the maze?";
		ButtonTexts = new string[] { "Yes", "No" };
		ButtonActions = new Action[] { () => { ExitTheMaze(); }, () => { } };

		confirmationManager.StartConfirmation(this);
	}
	#endregion
}
