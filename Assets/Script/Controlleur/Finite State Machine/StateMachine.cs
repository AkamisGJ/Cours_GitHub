using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState _currentState = null;

    public void ChangeState(IState newState){
        if(_currentState != null){
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter();
    }


    public void Update(){
        if(_currentState != null){
            _currentState.Update();
        }
    }
}
