
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class UpgradeStateManager : MonoBehaviour
{
    public GameObject uiCirclePlusBoxPrefab;
    
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
