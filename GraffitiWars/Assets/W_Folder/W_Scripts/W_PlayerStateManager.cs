using UnityEngine;

public class W_PlayerStateManager : MonoBehaviour
{

    W_PlayerBaseState currentState;
    public W_PlayerIdleState IdleState = new W_PlayerIdleState();
    public W_PlayerWalkState WalkState = new W_PlayerWalkState();
    public W_PlayerAttackState AttackState = new W_PlayerAttackState();
    public W_PlayerBlockState BlockState = new W_PlayerBlockState();
    public W_PlayerJumpState JumpState = new W_PlayerJumpState();
    public static W_PlayerStateManager Main;
  
    void Start()
    {
        Main = this;

        currentState = IdleState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(W_PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
