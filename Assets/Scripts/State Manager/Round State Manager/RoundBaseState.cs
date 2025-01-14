using UnityEngine;

public abstract class RoundBaseState
{
    public abstract void EnterState(RoundStateManager roundStateManager);
    public abstract void UpdateState(RoundStateManager roundStateManager);
    public abstract void OnCollisionEnter2D(RoundStateManager roundStateManager, Collision2D other);
}
