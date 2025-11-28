using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f; // ค่าความนุ่มของการลดความเร็ว

    private float currentSpeed = 0f;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontal = InputManager.Instance.GetHorizontal();

        if (Mathf.Abs(horizontal) > 0.01f)
        {
            // ค่อยๆ เปลี่ยน currentSpeed ไปยังความเร็วเป้าหมาย
            currentSpeed = Mathf.Lerp(currentSpeed, horizontal * moveSpeed, 0.2f);
        }
        else
        {
            // ค่อยๆ ลดความเร็วไป 0 แบบนุ่มๆ
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, smoothTime);
        }

        Vector3 moveDir = new Vector3(currentSpeed, 0f, 0f);

        controller.Move(moveDir * Time.deltaTime);
    }
}