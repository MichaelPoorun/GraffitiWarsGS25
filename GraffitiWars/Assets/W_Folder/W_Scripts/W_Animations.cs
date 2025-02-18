using UnityEngine;

public class W_Animations : MonoBehaviour
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayIdleAnimation()
    {
        animator.Play("Idle");
    }

    public void PlayForwardWalkAnimation()
    {
        animator.Play("ForwardWalk");
    }

    public void PlayLeftWalkAnimation()
    {
        animator.Play("LeftWalk");
    }

    public void PlayBackWalkAnimation()
    {
        animator.Play("BackWalk");
    }

    public void PlayRightWalkAnimation()
    {
        animator.Play("RightWalk");
    }

    public void PlayJumpAnimation()
    {
        animator.Play("Jump");
    }

    public void PlayBlockAnimation()
    {
        animator.Play("Block");
    }

    public void PlayAttackAnimation()
    {
        animator.Play("Punch");
    }
}
