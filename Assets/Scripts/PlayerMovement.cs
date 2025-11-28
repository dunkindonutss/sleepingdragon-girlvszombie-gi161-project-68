using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 5f;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontal = InputManager.Instance.GetHorizontal();

        // เดินบนแกน X
        Vector3 moveDir = new Vector3(horizontal, 0f, 0f);

        controller.Move(moveDir * moveSpeed * Time.deltaTime);
    }
}