using UnityEngine;
using System.Collections;
using MoreMountains.Feedbacks;

public class Zombie_Regular : Enemy, IPatrolable
{
    [Header("FeedBack")] [SerializeField] 
    private MMF_Player attackFeedBack;
    [Header("Patrol Settings")]
    [SerializeField] private Transform[] patrolTargets;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float reachThreshold = 0.1f;

    [Header("Detection Settings")]
    [SerializeField] private LayerMask playerLayer;

    private int currentPatrolIndex = 0;
    private Transform player;

    private void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        ChangeState(EnemyState.Patrol);
        StartCoroutine(StateRoutine());
    }

    private IEnumerator StateRoutine()
    {
        while (true)
        {
            if (CurrentState == EnemyState.Dead) yield break;

            switch (CurrentState)
            {
                case EnemyState.Idle:
                    // ยืนนิ่ง
                    break;

                case EnemyState.Patrol:
                    Patrol();
                    DetectPlayer();
                    break;

                case EnemyState.Chase:
                    ChasePlayer();
                    break;

                case EnemyState.Attack:
                    yield return AttackRoutine();
                    break;
            }

            yield return null;
        }
    }

    private void DetectPlayer()
    {
        if (CurrentState == EnemyState.Dead || player == null) return;

        Vector3 rayOrigin = transform.position + Vector3.up * 1f;
        Vector3 rayDir = transform.forward;

        bool hitPlayer = Physics.Raycast(rayOrigin, rayDir, out RaycastHit hit, detectRange, playerLayer);

        // Debug Raycast
        Color rayColor = hitPlayer ? Color.green : Color.red;
        Debug.DrawRay(rayOrigin, rayDir * detectRange, rayColor);

        if (hitPlayer)
        {
            if (CurrentState != EnemyState.Chase && CurrentState != EnemyState.Attack)
                ChangeState(EnemyState.Chase);
        }
        else
        {
            if (CurrentState == EnemyState.Chase)
                ChangeState(EnemyState.Patrol);
        }
    }

    private void ChasePlayer()
    {
        if (CurrentState == EnemyState.Dead || player == null) return;

        Player playerScript = player.GetComponent<Player>();

        // ถ้าผู้เล่นตาย → Idle
        if (playerScript != null && playerScript.IsDead)
        {
            ChangeState(EnemyState.Idle);
            return;
        }

        MoveToTarget(player.position);
        RotateTowardsTarget(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= 1.5f && playerScript != null && !playerScript.IsDead)
        {
            ChangeState(EnemyState.Attack);
        }
    }

    public override void Attack()
    {
        if (player == null) return;

        // Trigger Animation Attack
        animator.SetTrigger("Attack");

        // ลด HP Player
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.TakeDamage((int)attackPower);
            attackFeedBack.PlayFeedbacks();
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (CurrentState == EnemyState.Attack && !IsDead)
        {
            if (player == null) yield break;

            Player playerScript = player.GetComponent<Player>();

            // ถ้าผู้เล่นตาย → Idle
            if (playerScript == null || playerScript.IsDead)
            {
                ChangeState(EnemyState.Idle);
                yield break;
            }

            float distance = Vector3.Distance(transform.position, player.position);

            // ถ้าผู้เล่นออกจากระยะ → Chase
            if (distance > 1.5f)
            {
                ChangeState(EnemyState.Chase);
                yield break;
            }

            // Attack player
            Attack();

            // รอ Animation จบ
            float attackTime = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(attackTime);

            // จะวนโจมตีต่อถ้า player ยังไม่ตายและยังอยู่ในระยะ
        }
    }

    public void Patrol()
    {
        if (CurrentState == EnemyState.Dead || patrolTargets.Length == 0) return;

        if (CurrentState != EnemyState.Patrol)
            ChangeState(EnemyState.Patrol);

        Vector3 target = patrolTargets[currentPatrolIndex].position;
        MoveToTarget(target);
        RotateTowardsTarget(target);

        if (Vector3.Distance(transform.position, target) < reachThreshold)
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolTargets.Length;
    }

    private void MoveToTarget(Vector3 target)
    {
        if (CurrentState == EnemyState.Dead) return;

        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0f;
        transform.position += direction * moveSpeed * Time.deltaTime;

        animator.SetBool("isMoving", direction.magnitude > 0.01f);
    }

    private void RotateTowardsTarget(Vector3 target)
    {
        if (CurrentState == EnemyState.Dead) return;

        Vector3 dir = target - transform.position;
        dir.y = 0f;
        if (dir == Vector3.zero) return;

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 360f * Time.deltaTime);
    }

    protected override void OnDeath()
    {
        ChangeState(EnemyState.Dead);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null) boxCollider.enabled = false;
    }
}