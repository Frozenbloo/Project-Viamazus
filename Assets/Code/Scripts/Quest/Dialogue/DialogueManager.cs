using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI npcName;
    public GameObject dialogueBox;
    public float dialogueSpeed;
    public float maxDist = 0.32f;

    public Transform playerTransform;

    private string sentence;
    private IDialogue dialogue;
    private GameObject dialogueGameObject;

    private ViamazusQueue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new ViamazusQueue<string>();
        NullDialogue();
    }

	// Update is called once per frame
	void Update()
	{
        if (dialogue != null)
        {
			if (Input.GetMouseButtonDown(0))
			{
				if (text.text == this.sentence)
				{
					DisplayNextSentence();
				}
				else
				{
					StopAllCoroutines();
					text.text = this.sentence;
				}
			}
			if (Vector2.Distance(playerTransform.position, dialogueGameObject.transform.position) >= maxDist) EndDialogue();
		}
	}

	public void StartDialogue(IDialogue dialogue, GameObject dialogueGameObject)
    {
        this.dialogue = dialogue;
        this.dialogueGameObject = dialogueGameObject;
        npcName.text = dialogue.npcName;
        dialogueBox.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogue.dialogue) 
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        this.sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeDialogue(sentence));
    }

    IEnumerator TypeDialogue(string sentence)
    {
        text.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            text.text += letter;
			yield return new WaitForSeconds(dialogueSpeed);
		}
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        NullDialogue();
    }

    private void NullDialogue()
    {
		dialogueBox.SetActive(false);
		dialogue = null;
		text.text = string.Empty;
		npcName.text = string.Empty;
	}
}
