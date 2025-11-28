using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    public GameObject bulletPrefab;
    public float BulletForce;
    public int Damage;
    public float LifeTime;
}
