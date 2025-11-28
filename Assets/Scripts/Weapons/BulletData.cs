using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    [SerializeField] private GameObject bulletPrefab;
    public float BulletForce;
    public int Damage;
    public float LifeTime;
}
