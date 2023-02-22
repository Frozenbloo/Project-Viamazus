using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestStatus { UNAVAILABLE, UNASSIGNED, ASSIGNED, COMPLETE };
    public QuestStatus status = QuestStatus.UNAVAILABLE;
    public string questName;
}
