using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuestManager.Instance.AddQuest(0);
        QuestManager.Instance.AddQuest(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
