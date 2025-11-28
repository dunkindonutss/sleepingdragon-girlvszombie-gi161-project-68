using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string WeaponName;
    public Sprite WeaponIcon;
    public GameObject WeaponPrefab;
    public int WeaponDamage;
    public BulletData Bullet;
    public float WeaponFireRate;
    public int MagazineSize;
}
