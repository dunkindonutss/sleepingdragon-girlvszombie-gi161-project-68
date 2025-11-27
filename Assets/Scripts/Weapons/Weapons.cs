using System;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    public int WeaponDamage { get; private set; }
    private BulletData bulletData;
    public float WeaponFireRate { get; private set; }
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
