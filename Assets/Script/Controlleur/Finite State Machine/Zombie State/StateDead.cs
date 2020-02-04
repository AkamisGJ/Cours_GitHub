using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDead : IState
{

    private Monster _monster = null;

    public StateDead(Monster monster){
        this._monster = monster;
    }
    public void Enter(){
        _monster.Dead();
        // Debug.Log( _monster.name + " is Dead");
    }

    public void Update(){

    }

    public void Exit(){

    }
}
