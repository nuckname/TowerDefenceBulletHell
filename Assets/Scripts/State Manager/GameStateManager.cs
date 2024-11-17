using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    //What ever state we are currently in
    private GameBaseState _currentState;

    public RoundOverGameState _roundOverGameState = new RoundOverGameState();
    public BeginGameState _beginGameState = new BeginGameState();
    void Start()
    {
        _currentState = _roundOverGameState;
        _currentState.EnterState(this);
        
    }

    private void OnCollisionEnter(Collision col)
    {
        _currentState.OnCollisionEnter(this, col);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
        
    }
}
