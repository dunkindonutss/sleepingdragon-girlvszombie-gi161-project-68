using System;
using RootMotion.FinalIK;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : Character, IShooter
{
    public static Player Instance;

    [field: SerializeField] public Weapons CurrentWeapon { get; private set; }
    public AimIK _AimIK;
    public FullBodyBipedIK _FullBodyBipedIK;

    private CharacterController controller;
    private Animator animator;
    private PlayerMovement movement;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (IsDead) return;

        HandleState();

        // Shoot / Reload input
        if (InputManager.Instance.TryShoot())
        {
            Shoot();
        }

        if (InputManager.Instance.TryReload())
        {
            ReloadGun();
        }

        if (InputManager.Instance.TryChangeWeapon())
        {
            int nextIndex = (Inventory.Instance.currentWeaponIndex + 1) % Inventory.Instance.WeaponsList.Count;
            Inventory.Instance.currentWeaponIndex = nextIndex;
            ChangeWeapon(nextIndex);
        }
    }

    private void HandleState()
    {
        Vector2 moveInput = InputManager.Instance.GetMove();

        if (IsDead) return;

        // Move / Idle
        if (moveInput.magnitude > 0.01f)
        {
            ChangeState(CharacterState.Moving);
            movement.Move(moveInput);
        }
        else if (State == CharacterState.Moving)
        {
            ChangeState(CharacterState.Idle);
        }

        // อัปเดต Blend Tree float
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
    }

    public void ChangeWeapon(Weapons weaponToChange)
    {
        CurrentWeapon = weaponToChange;
    }

    public void ChangeWeapon(int index)
    {
        CurrentWeapon = Inventory.Instance.WeaponsList[index];
    }

    public void ReloadGun()
    {
        if (CurrentWeapon == null) return;

        ChangeState(CharacterState.Reloading);

        int weaponIndex = Inventory.Instance.WeaponsList.IndexOf(CurrentWeapon);
        if (weaponIndex < 0) return;

        int bulletsInInventory = Inventory.Instance.CountAmmo(weaponIndex);
        int bulletsNeeded = CurrentWeapon.MagazineSize - CurrentWeapon.BulletInGun;

        if (bulletsInInventory >= bulletsNeeded)
        {
            CurrentWeapon.ReloadMagazine(bulletsNeeded);
            Inventory.Instance.AddAmmo(weaponIndex, -bulletsNeeded);
        }
        else
        {
            CurrentWeapon.ReloadMagazine(bulletsInInventory);
            Inventory.Instance.AddAmmo(weaponIndex, -bulletsInInventory);
        }

        UIManager.Instance.RefreshWeaponUI();
        animator.SetTrigger("Reload");

        // หลังรีโหลดกลับ Idle หรือ Moving
        if (movement.IsMoving)
            ChangeState(CharacterState.Moving);
        else
            ChangeState(CharacterState.Idle);
    }

    public void Shoot()
    {
        if (CurrentWeapon == null) return;

        ChangeState(CharacterState.Attacking);
        CurrentWeapon.Fire();
        animator.SetTrigger("Shoot");

        // หลังยิงกลับ Idle / Moving
        if (movement.IsMoving)
            ChangeState(CharacterState.Moving);
        else
            ChangeState(CharacterState.Idle);

        UIManager.Instance.RefreshWeaponUI();
    }

    protected override void OnDeath()
    {
        ChangeState(CharacterState.Dead);
        movement.enabled = false;
        CurrentWeapon = null;
        // ปิดการยิงและรีโหลด
    }
    
    protected new void ChangeState(CharacterState newState)
    {
        if (State == newState) return;
        State = newState;
        animator.SetLayerWeight(1,1);

        // อัปเดต Animator ตาม State
        switch (State)
        {
            case CharacterState.Idle:
                animator.SetBool("IsMoving", false);
                break;
            case CharacterState.Moving:
                animator.SetBool("IsMoving", true);
                break;
            case CharacterState.Attacking:
                animator.SetTrigger("Shoot"); // หรือใช้ Trigger ของยิง
                break;
            case CharacterState.Reloading:
                animator.SetTrigger("Reload");
                break;
            case CharacterState.Pickup:
                animator.SetTrigger("IsPickup");
                animator.SetLayerWeight(1,0);
                _FullBodyBipedIK.solver.rightHandEffector.positionWeight = 0;
                _FullBodyBipedIK.solver.leftHandEffector.positionWeight = 0;
                break;
            case CharacterState.Dead:
                animator.SetBool("IsMoving", false);
                CharacterController cc = GetComponent<CharacterController>();
                Destroy(cc);
                animator.SetTrigger("IsDead");
                UIManager.Instance.OpenPanel(1);
                animator.SetLayerWeight(1,0);
                GunIKController gunIKController = GetComponent<GunIKController>();
                _AimIK.Disable();
                _FullBodyBipedIK.solver.rightHandEffector.positionWeight = 0;
                _FullBodyBipedIK.solver.leftHandEffector.positionWeight = 0;
                break;
        }
    }
}