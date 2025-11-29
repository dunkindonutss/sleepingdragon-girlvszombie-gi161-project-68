using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public static QuestUI Instance;

    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject questTextPrefab;

    private List<GameObject> currentUIElements = new List<GameObject>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RefreshUI()
    {
        foreach(var ui in currentUIElements)
            Destroy(ui);
        currentUIElements.Clear();

        foreach(var quest in QuestManager.Instance.activeQuests)
        {
            GameObject go = Instantiate(questTextPrefab, contentParent);
            TMP_Text text = go.GetComponent<TMP_Text>();
            text.text = $"- {quest.data.description} ({quest.currentProgress}/{quest.data.maxProgress})";
            currentUIElements.Add(go);
        }
    }
}