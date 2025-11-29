using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f;

    private float currentSpeedX = 0f;
    private float currentSpeedY = 0f;

    public bool IsMoving => Mathf.Abs(currentSpeedX) > 0.01f || Mathf.Abs(currentSpeedY) > 0.01f;

    public void Move(Vector2 input)
    {
        currentSpeedX = Mathf.Lerp(currentSpeedX, input.x * moveSpeed, 0.2f);
        currentSpeedY = Mathf.Lerp(currentSpeedY, input.y * moveSpeed, 0.2f);

        Vector3 moveDir = new Vector3(currentSpeedX, 0f, currentSpeedY);
        controller.Move(moveDir * Time.deltaTime);
    }
}