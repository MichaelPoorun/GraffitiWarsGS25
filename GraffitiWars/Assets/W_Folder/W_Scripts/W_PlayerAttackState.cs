using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class W_PlayerAttackState : W_PlayerBaseState
{


    private W_Animations Animations;
    private Animator animator;
    public bool CurrentlyAttacking;

    private W_PlayerStateManager player;

    //private W_PlayerHitMarker HitMarker;
    public HealthSystem Fight;

    public override void EnterState(W_PlayerStateManager player)
    {
        
        Debug.Log("Currently In Attacking State");
        animator = player.GetComponent<Animator>();
        Animations = player.GetComponent<W_Animations>();

        CurrentlyAttacking = true;

        this.player = player;

        
    }

    public override void UpdateState(W_PlayerStateManager player)
    {

       if (CurrentlyAttacking == true)
       {
            //HitMarker.TurnOnBox();
            Animations.PlayAttackAnimation();
            player.StartCoroutine(GoBackIdle());
       }

       if (CurrentlyAttacking == false)
       {
            //HitMarker.TurnOffBox();
            player.SwitchState(player.IdleState);
       }

    }

    IEnumerator GoBackIdle()
    {
        yield return new WaitForSeconds(.8f);
        CurrentlyAttacking = false;
    }

}
