using UnityEngine;

public enum CharacterState
{
    Idle,
    Moving,
    Attacking,
    Reloading,
    Pickup,
    Dead
}

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public int Health { get; private set; }
    public bool IsDead => Health <= 0;
    public CharacterState State { get; protected set; } = CharacterState.Idle;

    public virtual void TakeDamage(int damage)
    {
        if (IsDead) return;

        Health -= damage;
        if (Health <= 0)
        {
            OnDeath();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead) return;
        Health += amount;
    }

    protected virtual void OnDeath()
    {
        ChangeState(CharacterState.Dead);
    }

    protected void ChangeState(CharacterState newState)
    {
        if (State == newState) return;
        State = newState;
    }
}