using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGate : Item,IInteractable
{
   [SerializeField] private GameObject interactUI;
   [SerializeField] private Animator animator;
   [SerializeField] private GameObject winPanel;

   public bool canInteract = false;
   public bool canOpen = false;
   private bool isInteracted = false;

   public void Interact()
   {
      QuestManager.Instance.activeQuests[2].AddProgress(1);
      animator.SetTrigger("Interact");
      winPanel.SetActive(true);
      UIManager.Instance.SetCursorState(true);
   }

   private void Update()
   {
      // เช็กทุกเฟรมว่าถ้าผู้เล่นอยู่ใน Trigger และกด Interact
      if (canInteract && !isInteracted && Input.GetKeyDown(KeyCode.E) && canOpen)
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
         interactUI.SetActive(true);
         canOpen = true;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         canInteract = false;
         interactUI.SetActive(false);
         canOpen = false;
      }
   }
}
