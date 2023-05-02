using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPrincessNPC : Interactable, IDialogue, ISave
{
	private bool tutorialComplete, tutorialStarted;

	private string[] pDialogue =
	{
		"Greetings, noble adventurer!",
		"I am a princess in great need of your help.",
		"I've been taken from my kingdom and hidden away in a series of increasingly difficult mazes.",
		"I have faith in your abilities to navigate these treacherous labyrinths and find me.",
		"Before we begin, let's review some essential skills.",
		"To progress through dialogue, simply click your mouse button, and the conversation will advance.",
		"Remember to use the 'W', 'A', 'S', and 'D' keys to traverse the world, and press 'E' to interact with objects.",
		"Now, I must give you a weapon to aid you in your quest.",
		"Here, take this sword. I apologize for its poor condition, but it's all I have to offer.",
		"In time, you may find ways to improve it or even replace it entirely.",
		"As you embark on this perilous journey, keep in mind that things may not always be as they seem.",
		"Pay close attention to your surroundings",
		"...you may discover that the world you're venturing through is more intricate and mysterious than it first appears.",
		"And so, brave adventurer, with this humble sword in hand, I wish you good fortune.",
		"May you overcome the challenges that lie ahead and ultimately find me, so I can be returned to my kingdom.",
		"Be vigilant, and never lose hope.",
		"Farewell for now, and may the light guide you through the shadows."
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
