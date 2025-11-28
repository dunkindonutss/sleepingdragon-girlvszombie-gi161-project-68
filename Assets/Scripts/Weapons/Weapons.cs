using System;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    [SerializeField] public WeaponData weaponData;
    [Header("IK")]
    [SerializeField] private Transform l_handEffector;
    [SerializeField] private Transform r_handEffector;
    [Header("Info")]
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    public int WeaponDamage { get; private set; }
    private BulletData bulletData;
    public float WeaponFireRate { get; private set;}
    public int MagazineSize { get; private set;}
    public int BulletInGunCount {get; private set;}
    private IShooter Shooter;

    private void Start()
    {
        InitializeWeapon();
    }

    public void InitializeWeapon()
    {
        weaponName = weaponData.WeaponName;
        weaponPrefab = weaponData.WeaponPrefab;
        AdjustWeaponDamage(weaponData.WeaponDamage);
        bulletData = weaponData.Bullet;
        WeaponFireRate = weaponData.WeaponFireRate;
        AdjustMagazineSize(weaponData.MagazineSize);
    }

    public void AdjustMagazineSize(int magazineSize)
    {
        MagazineSize = magazineSize;
    }

    public void AdjustWeaponDamage(int damage)
    {
        WeaponDamage = damage;
    }
    
    protected virtual void BulletMove()
    {
        
    }

    public void OnHitWith(Character character)
    {
        character.TakeDamage(WeaponDamage);
    }

    public int GetShootDirection()
    {
        return Mathf.RoundToInt(transform.eulerAngles.z);
    }
    

}
