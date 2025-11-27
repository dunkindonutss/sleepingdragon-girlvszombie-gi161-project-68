using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    [SerializeField] private GameObject bulletPrefab;
    public float BulletForce {get; private set;}
    public int Damage {get; private set;}
    public float LifeTime {get; private set;}

    public void AdjustBulletForce(float force)
    {
        BulletForce = force;
    }

    public void AdjustDamage(int damage)
    {
        Damage = damage;
    }

    public void AdjustLifeTime(float time)
    {
        LifeTime = time;
    }
}
