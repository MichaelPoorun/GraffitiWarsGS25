using UnityEngine;

public class W_PlayerIdleState : W_PlayerBaseState
{
    private W_Animations Animations;
    private Animator animator;

    private W_Dumovementscriptfixed Jumping;

    public bool CurrentlyWalking;
    public bool CurrentlyAttacking;

    public W_PlayerHitMarker HitMarker;

  
    public override void EnterState(W_PlayerStateManager player)
    {
        Debug.Log("Im am currently in IDLESTATE");
        HitMarker = player.GetComponent<W_PlayerHitMarker>();
        HitMarker.TurnOffBox();

        CurrentlyWalking = false;
        CurrentlyAttacking = false;
        
        animator = player.GetComponent<Animator>();
        Animations = player.GetComponent<W_Animations>();
        Jumping = player.GetComponent<W_Dumovementscriptfixed>();
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

        if (Input.GetKey(KeyCode.Space) && Jumping.OnGround == true)
        {
            player.SwitchState(player.JumpState);
        }

        if (Input.GetMouseButtonDown(1))
        {
            player.SwitchState(player.BlockState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            CurrentlyAttacking = true;
        }

        if (CurrentlyAttacking == true)
        {
            player.SwitchState(player.AttackState);
        }
    }

    
}
