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

    public void PlayWalkAnimation()
    {
        animator.Play("Walk");
    }
}
