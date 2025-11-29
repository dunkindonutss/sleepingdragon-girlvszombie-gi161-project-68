using UnityEngine;

public class M4A1 : Weapons
{
    [SerializeField] private float RecoilForce;
    [SerializeField] private TargetAim _targetAim;
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

        GameObject bulletObj = Instantiate(
            bulletData.bulletPrefab,
            bulletSpawnPoint.position,
            bulletSpawnPoint.rotation
        );
        
        _targetAim.AddRecoil(RecoilForce);

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.bulletData = bulletData;

        bullet.Initialize();
        bullet.BulletMove();
    }
}
