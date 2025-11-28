using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerControls input;

    private Vector2 moveInput;
    private bool crouchPressed;

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
    }

    private void OnDisable()
    {
        input.Movement.Move.performed -= OnMove;
        input.Movement.Move.canceled  -= OnMoveCanceled;
        input.Disable();
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    public float GetHorizontal() => moveInput.x;   // <— ใช้เดินซ้ายขวา
    public Vector2 GetMove() => moveInput;
    public bool IsCrouching() => crouchPressed;
}