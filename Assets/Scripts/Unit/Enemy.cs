using UnityEngine;

public abstract class Enemy : Character
{
     [SerializeField] private Player player;
     public bool IsDetectedPlayer {get; private set;}
     public float detectRange {get; private set;}

     public void AdjustDetectRange(float detectRange)
     {
          this.detectRange = detectRange;
     }

     public abstract void Attack();
}
