using UnityEngine;

[System.Serializable]
public class Quest
{
    public QuestData data;
    public int currentProgress;

    public bool IsCompleted => currentProgress >= data.maxProgress;

    public void AddProgress(int amount = 1)
    {
        currentProgress += amount;
        if(currentProgress > data.maxProgress) currentProgress = data.maxProgress;
    }
}