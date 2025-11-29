using UnityEngine;
using System.Collections;

public class Zombie_Regular : Enemy, IPatrolable
{
    [Header("Patrol Settings")]
    [SerializeField] private Transform[] patrolTargets; 
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float reachThreshold = 0.1f;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 10f; // ระยะ Raycast
    [SerializeField] private LayerMask playerLayer;     // เลเยอร์ของผู้เล่น

    [Header("Components")]
    [SerializeField] private Animator animator;

    private int currentPatrolIndex = 0;
    private bool isChasingPlayer = false;
    private Transform player;

    private void Start()
    {
        // หาผู้เล่น
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        StartCoroutine(PatrolRoutine());
    }

    private void Update()
    {
        DetectPlayer();

        if (isChasingPlayer && player != null)
        {
            MoveToTarget(player.position);
            RotateTowardsTarget(player.position);
        }
    }

    // ตรวจจับผู้เล่นด้วย Raycast จากด้านหน้า Zombie
    private void DetectPlayer()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 1f; // สูงประมาณเอว
        Vector3 rayDir = transform.forward; // ด้านหน้าตัว Zombie
        Ray ray = new Ray(rayOrigin, rayDir);

        if (Physics.Raycast(ray, out RaycastHit hit, detectionRange, playerLayer))
        {
            if (!isChasingPlayer)
                StartChasePlayer();
        }
        else
        {
            if (isChasingPlayer)
                StopChasePlayer();
        }

        // Debug Ray ใน SceneView
        Debug.DrawRay(ray.origin, ray.direction * detectionRange, Color.red);
    }

    // เดินไปหาจุดหรือผู้เล่น (แกน X)
    public void MoveToTarget(Vector3 target)
    {
        Vector3 direction = new Vector3(target.x - transform.position.x, 0f, 0f);
        if (direction.magnitude > 0.01f)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    // หมุนเนียนไปหาทิศ target
    private void RotateTowardsTarget(Vector3 target)
    {
        float targetY = target.x > transform.position.x ? 90f : -90f;
        Quaternion targetRot = Quaternion.Euler(0f, targetY, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 360f * Time.deltaTime);
    }

    // ระบบ Patrol
    public void Patrol()
    {
        if (patrolTargets.Length == 0) return;

        Vector3 target = patrolTargets[currentPatrolIndex].position;
        RotateTowardsTarget(target);
        MoveToTarget(target);

        if (Mathf.Abs(transform.position.x - target.x) < reachThreshold)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolTargets.Length;
        }
    }

    private IEnumerator PatrolRoutine()
    {
        while (!isChasingPlayer)
        {
            Patrol();
            yield return null;
        }
    }

    public override void Attack()
    {
        Debug.Log("Zombie Attack!");
    }

    public void StartChasePlayer()
    {
        isChasingPlayer = true;
        StopCoroutine(PatrolRoutine());
    }

    public void StopChasePlayer()
    {
        isChasingPlayer = false;
        StartCoroutine(PatrolRoutine());
    }
}