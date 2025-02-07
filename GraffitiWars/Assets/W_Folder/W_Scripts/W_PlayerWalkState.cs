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
        if (CurrentlyWalking == true)
        {
            Animations.PlayWalkAnimation();
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
