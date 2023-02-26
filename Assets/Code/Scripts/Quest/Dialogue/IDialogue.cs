using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IDialogue
{
    public string[] dialogue { get; set; }
    public string npcName { get; set; }

    void OnDialogueEnd();
}
