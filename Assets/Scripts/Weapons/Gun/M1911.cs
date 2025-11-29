using UnityEngine;

public class M1911 : Weapons
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Fire()
    {
        if (BulletInGun <= 0)
        {
            Debug.Log("No bullets");
            return;
        }
        
        AdjustBulletInGun(-1);
        
        GameObject bulletObj = Instantiate(bulletData.bulletPrefab,
            bulletSpawnPoint.position,
            bulletSpawnPoint.rotation);
        
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.bulletData = bulletData;
        
        bullet.Initialize();   // ใส่ค่าจาก bulletData
        bullet.BulletMove();   // ยิงออกไป
    }
}
