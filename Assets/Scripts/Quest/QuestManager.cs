using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    
    public QuestData[] AllQuests;

    public List<Quest> activeQuests = new List<Quest>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        AddQuest(0);
    }

    public void AddQuest(QuestData questData)
    {
        Quest newQuest = new Quest { data = questData, currentProgress = 0 };
        activeQuests.Add(newQuest);
        QuestUI.Instance.RefreshUI();
    }

    public void AddQuest(int index)
    {
        Quest newQuest = new Quest { data = AllQuests[index], currentProgress = 0 };
        activeQuests.Add(newQuest);
        QuestUI.Instance.RefreshUI();
    }

    public void AddProgress(QuestData questData, int amount = 1)
    {
        Quest quest = activeQuests.Find(q => q.data == questData);
        if(quest != null)
        {
            quest.AddProgress(amount);
            QuestUI.Instance.RefreshUI();
        }
    }
}