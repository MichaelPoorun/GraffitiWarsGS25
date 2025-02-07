using UnityEngine;

public class W_PlayerIdleState : W_PlayerBaseState
{

    [SerializeField] private Animator Player;

    [SerializeField] private string IdleA = "Idle";

    public bool CurrentlyWalking;


    public override void EnterState(W_PlayerStateManager player)
    {
        Debug.Log("Im am currently in IDLESTATE");
        CurrentlyWalking = false;
    }

    public override void UpdateState(W_PlayerStateManager player)
    {
        if (Input.GetKey(KeyCode.W))
        {
            CurrentlyWalking = true;
        }

        if (CurrentlyWalking == true)
        {
            player.SwitchState(player.WalkState);
        }
        else
        {
            Playerr
        }

    }
}
