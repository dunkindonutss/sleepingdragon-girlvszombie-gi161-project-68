using UnityEngine;

public enum CharacterState
{
    Idle,
    Moving,
    Attacking,
    Reloading,
    PickUp,
    Dead
}
public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public CharacterState State {get; private set;}
    [SerializeField] private string name;
    [field: SerializeField] public int Health {get; private set;}
    

    public void ChangeState(CharacterState newState)
    {
        State = newState;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void Heal(int heal)
    {
        Health += heal;
    }

    public bool IsDead()
    {
        return State == CharacterState.Dead;
    }
    
}
