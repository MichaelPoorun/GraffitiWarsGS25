using UnityEngine;

public class W_PlayerBlockState : W_PlayerBaseState
{
    private W_Animations Animations;
    private Animator animator;

    public override void EnterState(W_PlayerStateManager player)
    {
        Debug.Log("Currently in BLOCK state");

        animator = player.GetComponent<Animator>();
        Animations = player.GetComponent<W_Animations>();
    }

    public override void UpdateState(W_PlayerStateManager player)
    {
        if (Input.GetMouseButton(1))
        {
            Animations.PlayBlockAnimation();
        }
        else
        {
            player.SwitchState(player.IdleState);
        }
    }
}
