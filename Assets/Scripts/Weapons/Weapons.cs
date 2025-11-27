using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    public int WeaponDamage { get; private set; }
    private BulletData bulletData;
    public float WeaponFireRate { get; private set; }
    private IShooter Shooter;
    
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
