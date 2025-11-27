using System;
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

        // Move
        input.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Movement.Move.canceled += ctx => moveInput = Vector2.zero;

        // Crouch
        input.Movement.Crouch.performed += ctx => crouchPressed = true;
        input.Movement.Crouch.canceled  += ctx => crouchPressed = false;
    }

    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();

    public Vector2 GetMove() => moveInput;

    public bool IsCrouching() => crouchPressed;
}