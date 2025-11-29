using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Rigidbody")]
    [SerializeField] private Rigidbody rb;

    [Header("Blood FX")]
    [SerializeField] private GameObject bloodEffectPrefab;

    [Header("Hit FeedBack")]
    [SerializeField] private MMF_Player hitFeedBack;

    [Header("Data")]
    public BulletData bulletData;

    private float bulletForce;
    private int damage;
    private float lifeTime;

    private void Start()
    {
        Initialize();
        Destroy(gameObject, lifeTime);
    }

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();

        bulletForce = bulletData.BulletForce;
        damage = bulletData.Damage;
        lifeTime = bulletData.LifeTime;
    }

    public void BulletMove()
    {
        rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
    }

    public void OnHitWith(Character character, Vector3 hitPos, Vector3 hitNormal)
    {
        character.TakeDamage(damage);
        Debug.Log($"{character.name} take damage {damage}");

        SpawnBloodEffect(hitPos, hitNormal);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hitPos = transform.position;
        Vector3 hitNormal = -transform.forward;

        // เล่น Hit Feedback ก่อนทำลายกระสุน
        if (hitFeedBack != null)
        {
            hitFeedBack.PlayFeedbacks(hitPos);
        }

        if (other.TryGetComponent<Character>(out var character))
        {
            OnHitWith(character, hitPos, hitNormal);
        }
        else
        {
            SpawnBloodEffect(hitPos, hitNormal);
        }

        Destroy(gameObject);
    }

    private void SpawnBloodEffect(Vector3 pos, Vector3 normal)
    {
        if (bloodEffectPrefab == null) return;

        Quaternion rot = Quaternion.LookRotation(normal);
        GameObject fx = Instantiate(bloodEffectPrefab, pos, rot);

        Destroy(fx, 2f); // ปรับตามต้องการ เช่น 1–3 วินาที
    }
}