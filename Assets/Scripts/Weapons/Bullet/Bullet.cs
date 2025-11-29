using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ParticleSystem particleOnHit;
    public BulletData bulletData;
    public float BulletForce;
    public int Damage;
    public float LifeTime;

    private void Start()
    {
        Initialize();

        // หายไปเมื่อหมดเวลา
        Destroy(gameObject, LifeTime);
    }

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        BulletForce = bulletData.BulletForce;
        Damage = bulletData.Damage;
        LifeTime = bulletData.LifeTime;
    }

    public void BulletMove()
    {
        // ยิงไปตามทิศทางของกระสุน
        rb.AddForce(transform.forward * BulletForce, ForceMode.Impulse);
    }

    public void OnHitWith(Character character)
    {
        character.TakeDamage(Damage);
        Debug.Log($"{character.name} take damage {Damage}");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // ถ้าเป็นตัวละคร → โดนยิง
        if (other.TryGetComponent<Character>(out var character))
        {
            OnHitWith(character);
            Instantiate(particleOnHit, transform.position, Quaternion.identity);
        }

        // หายไปเมื่อชน
        Destroy(gameObject);
    }
}