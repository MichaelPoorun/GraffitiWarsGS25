using UnityEngine;

public class W_PlayerWalkState : W_PlayerBaseState
{
    private W_Animations Animations;
    private Animator animator;

    public bool CurrentlyWalking;

    public override void EnterState(W_PlayerStateManager player)
    {
        Debug.Log("I am currently in WALKSTATE");
        CurrentlyWalking = true;

        animator = player.GetComponent<Animator>();
        Animations = player.GetComponent<W_Animations>();
    }

    public override void UpdateState(W_PlayerStateManager player)
    {
        if (CurrentlyWalking == true && (Input.GetKey(KeyCode.W)))
        {
            Animations.PlayLeftWalkAnimation();
        }
        else
        {
            player.SwitchState(player.IdleState);
        }

        if (CurrentlyWalking == true && (Input.GetKey(KeyCode.A)))
        {
            Animations.PlayBackWalkAnimation();
        }
        else
        {
            player.SwitchState(player.IdleState);
        }

        if (CurrentlyWalking == true && (Input.GetKey(KeyCode.S)))
        {
            Animations.PlayRightWalkAnimation();
        }
        else
        {
            player.SwitchState(player.IdleState);
        }

        if (CurrentlyWalking == true && (Input.GetKey(KeyCode.D)))
        {
            Animations.PlayForwardWalkAnimation();
        }
        else
        {
            player.SwitchState(player.IdleState);
        }

        if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D))
        {
            CurrentlyWalking = false;
        }
    }
}
