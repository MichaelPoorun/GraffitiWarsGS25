using UnityEngine;

public abstract class W_PlayerBaseState
{
    public abstract void EnterState(W_PlayerStateManager player);

    public abstract void UpdateState(W_PlayerStateManager player);
}
