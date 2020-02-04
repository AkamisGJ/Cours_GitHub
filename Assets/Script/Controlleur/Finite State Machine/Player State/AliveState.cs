using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveState : IState
{
    private PlayerControlleur _player = null;

    public AliveState(PlayerControlleur player){
        this._player = player;
    }
    
    public void Enter(){
        
    }

    public void Update(){
        _player.Aiming();
    }

    public void Exit(){
        
    }
}
