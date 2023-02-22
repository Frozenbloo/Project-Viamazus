using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

	public Quest[] Quests;

	private void Awake()
	{
		if (QuestManager.instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;

		DontDestroyOnLoad(gameObject);
	}

	public static Quest.QuestStatus GetQuestStatus(string QuestName)
	{
		foreach (Quest quest in instance.Quests)
		{
			if (quest.questName.Equals(QuestName))
			{
				return quest.status;
			}
		}
		return Quest.QuestStatus.UNASSIGNED;
	}
	public static void SetQuestStatus(string QuestName, Quest.QuestStatus NewStatus)
	{
		foreach (Quest quest in instance.Quests)
		{
			if (quest.questName.Equals(QuestName))
			{
				quest.status = NewStatus; return;
			}
		}
	}
}
