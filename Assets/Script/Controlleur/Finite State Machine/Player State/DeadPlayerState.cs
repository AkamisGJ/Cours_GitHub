using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerState : IState
{
    private PlayerControlleur _player = null;

    public DeadPlayerState(PlayerControlleur player){
        this._player = player;
    }
    
    public void Enter(){
        _player.Dead();
    }

    public void Update(){
        
    }

    public void Exit(){
        
    }
}
