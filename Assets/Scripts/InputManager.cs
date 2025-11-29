using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerControls input;

    private Vector2 moveInput;
    private bool crouchPressed;
    private bool changeWeaponPressed;
    private bool reloadPressed;
    private bool shootPressed;
    private bool mainMenuPressed;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        input = new PlayerControls();
    }

    private void OnEnable()
    {
        input.Enable();

        // Movement
        input.Movement.Move.performed += OnMove;
        input.Movement.Move.canceled  += OnMoveCanceled;
        input.Movement.Crouch.performed += ctx => crouchPressed = true;
        input.Movement.Crouch.canceled  += ctx => crouchPressed = false;

        // GamePlay
        input.GamePlay.ChangeWeapon.performed += OnChangeWeapon;
        input.GamePlay.Reload.performed += OnReload;
        input.GamePlay.Shoot.performed += OnShoot;

        // UI
        input.UI.MainMenu.performed += ctx => mainMenuPressed = true;
    }

    private void OnDisable()
    {
        // Movement
        input.Movement.Move.performed -= OnMove;
        input.Movement.Move.canceled  -= OnMoveCanceled;
        input.Movement.Crouch.performed -= ctx => crouchPressed = true;
        input.Movement.Crouch.canceled  -= ctx => crouchPressed = false;

        // GamePlay
        input.GamePlay.ChangeWeapon.performed -= OnChangeWeapon;
        input.GamePlay.Reload.performed -= OnReload;
        input.GamePlay.Shoot.performed -= OnShoot;

        // UI
        input.UI.MainMenu.performed -= ctx => mainMenuPressed = true;

        input.Disable();
    }

    // Movement callbacks
    private void OnMove(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();
    private void OnMoveCanceled(InputAction.CallbackContext ctx) => moveInput = Vector2.zero;

    private void OnChangeWeapon(InputAction.CallbackContext ctx) => changeWeaponPressed = true;
    private void OnReload(InputAction.CallbackContext ctx) => reloadPressed = true;
    private void OnShoot(InputAction.CallbackContext ctx) => shootPressed = true;

    // ฟังก์ชันดึงค่า input
    public float GetHorizontal() => moveInput.x;
    public Vector2 GetMove() => moveInput;
    public Vector2 GetMouseDelta() => input.GamePlay.MouseDelta.ReadValue<Vector2>();
    public bool IsCrouching() => crouchPressed;

    public bool TryChangeWeapon()
    {
        if (changeWeaponPressed)
        {
            changeWeaponPressed = false;
            Debug.Log("ChangeWeapon performed");
            return true;
        }
        return false;
    }

    public bool TryReload()
    {
        if (reloadPressed)
        {
            reloadPressed = false;
            return true;
        }
        return false;
    }

    public bool TryShoot()
    {
        if (shootPressed)
        {
            shootPressed = false;
            return true;
        }
        return false;
    }

    public bool TryMainMenu()
    {
        if (mainMenuPressed)
        {
            mainMenuPressed = false;
            return true;
        }
        return false;
    }
}