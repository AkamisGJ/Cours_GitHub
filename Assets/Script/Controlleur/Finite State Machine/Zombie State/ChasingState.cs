using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : IState
{
    private Monster _monster = null;

    public ChasingState(Monster monster){
        this._monster = monster;
    }
    public void Enter(){
        // Debug.Log(_monster.name + " Begin to chase player");
    }

    public void Update(){
        _monster.Hunting();
    }

    public void Exit(){
        // Debug.Log(_monster.name + " Stop chasing player");
    }
}
