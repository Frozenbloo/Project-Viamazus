using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPrincessNPC : Interactable, IDialogue, ISave
{
	private bool tutorialComplete, tutorialStarted;

	private string[] pDialogue =
	{
		"I'm a princess!"
	};

	private string pNpcName = "Princess:";

	public string[] dialogue { get => pDialogue; set => pDialogue = value; }
	public string npcName { get => pNpcName; set => pNpcName = value; }

	public void LoadData(GameSave data)
	{
		tutorialComplete = data.tutorialCompleted;
	}

	public void OnDialogueEnd()
	{
		tutorialComplete = true;
		SceneManager.LoadSceneAsync("Hub");
	}

	public void SaveData(ref GameSave data)
	{
		data.tutorialCompleted = tutorialComplete;
	}

	protected override void OnCollide(Collider2D collider)
	{
		if (!tutorialStarted)
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this, gameObject);
			tutorialStarted = true;
		}
		else base.OnCollide(collider);
	}
}
