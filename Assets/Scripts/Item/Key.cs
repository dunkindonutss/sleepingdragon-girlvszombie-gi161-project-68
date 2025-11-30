using System;
using UnityEngine;

public class Key : Item, IInteractable
{
    [SerializeField] private GameObject interactUI;

    private bool canInteract = false;
    private bool isInteracted = false;
    [SerializeField] MainGate mainGate;

    public void Interact()
    {
        QuestManager.Instance.activeQuests[1].AddProgress(1);
        QuestManager.Instance.AddQuest(2);
        mainGate.canInteract = true;
        Destroy(this.gameObject);
    }

    private void Update()
    {
        // เช็กทุกเฟรมว่าถ้าผู้เล่นอยู่ใน Trigger และกด Interact
        if (canInteract && !isInteracted && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            isInteracted = true;
            interactUI.SetActive(false);   // ซ่อน UI หลังเก็บ key แล้ว
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInteracted)
        {
            canInteract = true;
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            interactUI.SetActive(false);
        }
    }
}