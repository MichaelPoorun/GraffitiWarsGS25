using UnityEngine;

public class W_PlayerJumpState : W_PlayerBaseState
{

    private W_Dumovementscriptfixed Jumping;
    private W_Animations Animations;
    private Animator animator;

    public override void EnterState(W_PlayerStateManager player)
    {
        Debug.Log("I am in JUMP state");

        animator = player.GetComponent<Animator>();
        Animations = player.GetComponent<W_Animations>();
        Jumping = player.GetComponent<W_Dumovementscriptfixed>();

        Animations.PlayJumpAnimation();
    }

    public override void UpdateState(W_PlayerStateManager player)
    {
        if (Jumping.OnGround == true)
        {
            player.SwitchState(player.IdleState);
        }
    }
}
