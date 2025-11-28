using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        SetMoveAnimation();
    }

    public void SetMoveAnimation()
    {
        float x = InputManager.Instance.GetHorizontal();

        anim.SetFloat("MoveX", x);
        anim.SetBool("IsMoving", Mathf.Abs(x) > 0.01f);
    }
}