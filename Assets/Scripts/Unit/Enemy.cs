using UnityEngine;

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Dead
}

public abstract class Enemy : Character
{
    [SerializeField] protected Animator animator;
    [SerializeField]public EnemyState CurrentState { get; protected set; } = EnemyState.Idle;

    public bool IsDetectedPlayer { get; protected set; }
    public float detectRange;
    [SerializeField] protected float attackPower;

    

    public abstract void Attack();

    protected void ChangeState(EnemyState newState)
    {
        if (CurrentState == newState) return;
        CurrentState = newState;

        // Animator Update
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Die");

        switch (newState)
        {
            case EnemyState.Idle:
                animator.SetBool("isMoving", false);
                break;
            case EnemyState.Patrol:
            case EnemyState.Chase:
                animator.SetBool("isMoving", true);
                break;
            case EnemyState.Attack:
                animator.SetBool("isMoving", false);
                animator.SetTrigger("Attack");
                break;
            case EnemyState.Dead:
                animator.SetBool("isMoving", false);
                animator.SetTrigger("Die");
                StopAllCoroutines(); // หยุด AI ทุกอย่าง
                break;
        }
    }

    protected override void OnDeath()
    {
        ChangeState(EnemyState.Dead);
    }
}