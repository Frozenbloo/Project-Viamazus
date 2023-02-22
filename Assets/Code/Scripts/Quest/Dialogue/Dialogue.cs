using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IDialogue
{
    public string[] dialogue { get; set; }
    /**
    public TextMeshProUGUI text;
    public string[] dialogueLines;
    public float dialogueSpeed;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

  

    private void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeDialogue());
    }

    private void NextDialogueLine()
    {
        if (index < dialogueLines.Length - 1) 
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeDialogue());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeDialogue()
    {
        foreach (char chars in dialogueLines[index].ToCharArray())
        {
            text.text += chars;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }
    **/
}
