using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/QuestData")]
public class QuestData : ScriptableObject
{
    public string questName;
    [TextArea] public string description;
    public int maxProgress = 1;
}