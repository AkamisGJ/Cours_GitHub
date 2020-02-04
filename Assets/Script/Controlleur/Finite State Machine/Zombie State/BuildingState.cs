using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : IState
{
    private Monster _monster = null;

    public BuildingState(Monster monster){
        this._monster = monster;
    }
    
    public void Enter(){
        // Debug.Log("Building a " +_monster.name + "at " + _monster.transform.position.ToString());
    }

    public void Update(){
        _monster.Building();
    }

    public void Exit(){
        // Debug.Log("Finish Building Step of " + _monster.name);
    }
}
