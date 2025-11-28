using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerControls input;

    private Vector2 moveInput;
    private bool crouchPressed;
    private bool changeWeaponPressed; // สำหรับตรวจสอบการกดครั้งเดียว

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        input = new PlayerControls();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Movement.Move.performed += OnMove;
        input.Movement.Move.canceled  += OnMoveCanceled;

        input.Movement.Crouch.performed += ctx => crouchPressed = true;
        input.Movement.Crouch.canceled  += ctx => crouchPressed = false;

        // เพิ่ม ChangeWeapon
        input.GamePlay.ChangeWeapon.performed += OnChangeWeapon;
    }

    private void OnDisable()
    {
        input.Movement.Move.performed -= OnMove;
        input.Movement.Move.canceled  -= OnMoveCanceled;

        input.GamePlay.ChangeWeapon.performed -= OnChangeWeapon;

        input.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    private void OnChangeWeapon(InputAction.CallbackContext ctx)
    {
        // ปุ่มถูกกดครั้งเดียว จะเซ็ต flag เป็น true
        changeWeaponPressed = true;
    }

    public float GetHorizontal() => moveInput.x;
    public Vector2 GetMove() => moveInput;
    public bool IsCrouching() => crouchPressed;

    // ฟังก์ชันสำหรับดึงค่าการกดเปลี่ยนอาวุธ
    public bool TryChangeWeapon()
    {
        if (changeWeaponPressed)
        {
            changeWeaponPressed = false; // รีเซ็ต flag หลัง trigger ครั้งเดียว
            return true;
        }
        return false;
    }
}