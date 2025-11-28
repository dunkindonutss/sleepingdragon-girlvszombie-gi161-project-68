using System;
using RootMotion.FinalIK;
using UnityEngine;

public class Player : Character,IShooter
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

    public void ChangeWeapon(Weapons WeaponToChange)
    {
        CurrentWeapon = WeaponToChange;
    }

    public void ChangeWeapon(int Index)
    {
        CurrentWeapon = Inventory.Instance.WeaponsList[Index];
    }

    public void Shoot(Weapons weapons)
    {
        GameObject Bullet = Instantiate(CurrentWeapon.weaponData.Bullet.bulletPrefab,CurrentWeapon.bulletSpawnPoint.position, Quaternion.identity);
    }
    
    
    
}
