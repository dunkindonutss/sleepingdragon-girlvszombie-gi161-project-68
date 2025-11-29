using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    [Header("FeedBack")] 
    [SerializeField] protected MMF_Player shootFeedBack;
    [SerializeField] protected MMF_Player reloadFeedBack;
    [SerializeField] public WeaponData weaponData;
    [Header("IK")]
    [SerializeField] public Transform l_handEffector;
    [SerializeField] public Transform r_handEffector;
    [SerializeField] private Transform aimTransform;
    [Header("Info")]
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] public Transform bulletSpawnPoint;

    public int WeaponDamage { get; private set; }
    protected BulletData bulletData;
    [field: SerializeField] public float WeaponFireRate { get; private set;}
    [field: SerializeField] public int MagazineSize { get; private set;}
    [field: SerializeField] public int BulletInGun { get; private set;}
    private IShooter Shooter;

    private void Start()
    {
        InitializeWeapon();
    }

    public void InitializeWeapon()
    {
        weaponName = weaponData.WeaponName;
        AdjustWeaponDamage(weaponData.WeaponDamage);
        bulletData = weaponData.Bullet;
        WeaponFireRate = weaponData.WeaponFireRate;
        AdjustMagazineSize(weaponData.MagazineSize);
    }

    public void AdjustMagazineSize(int magazineSize)
    {
        MagazineSize = magazineSize;
    }

    public void AdjustBulletInGun(int amount)
    {
        BulletInGun += amount;
    }

    public void AdjustWeaponDamage(int damage)
    {
        WeaponDamage = damage;
    }
    
    // เติมกระสุน โดยไม่ให้เกิน MagazineSize
    public void ReloadMagazine(int bulletCount)
    {
        BulletInGun = Mathf.Min(BulletInGun + bulletCount, MagazineSize);
        reloadFeedBack.PlayFeedbacks();
    }

    public abstract void Fire();
}