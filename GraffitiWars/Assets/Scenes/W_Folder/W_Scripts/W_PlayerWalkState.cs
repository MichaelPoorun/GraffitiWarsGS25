using UnityEngine;

public class W_PlayerWalkState : W_PlayerBaseState
{
    [SerializeField] private Animator Player;

    [SerializeField] private string WalkA = "Walk";

    public bool CurrentlyWalking;

    public override void EnterState(W_PlayerStateManager player)
    {
        Debug.Log("I am currently in WALKSTATE");
        CurrentlyWalking = true;
    }

    public override void UpdateState(W_PlayerStateManager player)
    {
        if (CurrentlyWalking == true)
        {
            Debug.Log("Walking Animation Is Playing");
        }
        else
        {
            player.SwitchState(player.IdleState);
        }

        if (!Input.GetKey(KeyCode.W))
        {
            CurrentlyWalking = false;
        }
    }
}
