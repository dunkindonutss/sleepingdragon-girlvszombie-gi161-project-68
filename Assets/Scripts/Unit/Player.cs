using System;
using RootMotion.FinalIK;
using UnityEngine;

public class Player : Character, IShooter
{
    public static Player Instance;
    [field: SerializeField] public Weapons CurrentWeapon {get; private set;}
    public AimIK _AimIK;
    public FullBodyBipedIK _FullBodyBipedIK;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        // ตรวจสอบ input reload
        if (InputManager.Instance.TryReload())
        {
            ReloadGun();
        }

        // ตัวอย่าง change weapon
        if (InputManager.Instance.TryChangeWeapon())
        {
            int nextIndex = (Inventory.Instance.currentWeaponIndex + 1) % Inventory.Instance.WeaponsList.Count;
            Inventory.Instance.currentWeaponIndex = nextIndex;
            ChangeWeapon(nextIndex);
        }
        
        if (InputManager.Instance.TryShoot())
        {
            Shoot();
        }
    }

    public void ChangeWeapon(Weapons WeaponToChange)
    {
        CurrentWeapon = WeaponToChange;
    }

    public void ChangeWeapon(int Index)
    {
        CurrentWeapon = Inventory.Instance.WeaponsList[Index];
    }

    public void ReloadGun()
    {
        if (CurrentWeapon == null) return;

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

        Debug.Log($"Reloaded {CurrentWeapon.weaponData.WeaponName}, Bullets in Gun: {CurrentWeapon.BulletInGun}, Bullets left in inventory: {Inventory.Instance.CountAmmo(weaponIndex)}");
    }

    public void Shoot()
    {
        if (CurrentWeapon == null) return;
        CurrentWeapon.Fire();
        UIManager.Instance.RefreshWeaponUI();
    }
}