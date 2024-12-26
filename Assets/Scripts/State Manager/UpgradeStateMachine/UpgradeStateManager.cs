
using System;
using UnityEngine;

public class UpgradeStateManager : MonoBehaviour
{
    private GameBaseState _currentState;

    public NoUpgradeSelectedState _noUpgradeSelectedState = new NoUpgradeSelectedState();
    public SingleUpgradeOptionSelectedState _singleUpgradeOptionState = new SingleUpgradeOptionSelectedState();
    public MultipleUpgradeChoicesAvailableState _multipleUpgradeChoicesState = new MultipleUpgradeChoicesAvailableState();

    void Start()
    {
        _currentState = _noUpgradeSelectedState;
        _currentState.EnterState(this);
    }

    void Update()
    {
        _currentState.UpdateState(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _currentState.OnTriggerEnter2D(this, collider);

    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        _currentState.OnTriggerExit2D(this, collider);

    }

    public void SwitchState(GameBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
    
    
}
