using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.activeQuests[0].currentProgress++;
            QuestManager.Instance.AddQuest(1);
            Destroy(this.gameObject);
        }
    }
}
