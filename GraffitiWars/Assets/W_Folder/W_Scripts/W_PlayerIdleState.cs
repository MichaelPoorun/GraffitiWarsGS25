using UnityEngine;

public class W_PlayerIdleState : W_PlayerBaseState
{
    private W_Animations Animations;
    private Animator animator;

    public bool CurrentlyWalking;

    public override void EnterState(W_PlayerStateManager player)
    {
        Debug.Log("Im am currently in IDLESTATE");
        CurrentlyWalking = false;

        animator = player.GetComponent<Animator>();
        Animations = player.GetComponent<W_Animations>();
    }

    public override void UpdateState(W_PlayerStateManager player)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            CurrentlyWalking = true;
        }

        if (CurrentlyWalking == true)
        {
            player.SwitchState(player.WalkState);
        }
        else
        {
            Animations.PlayIdleAnimation();
        }

    }

    
}
