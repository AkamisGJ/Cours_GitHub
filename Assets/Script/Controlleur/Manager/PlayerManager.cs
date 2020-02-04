using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
The Event name use the default name of the InputManager of Unity
*/

public class PlayerManager : Singleton<PlayerManager>
{
   [SerializeField] private GameObject _playerPrefab = null;

   private PlayerControlleur _player = null;
   public PlayerControlleur Player{
       get{
           return _player;
       }
       set{
           _player = value;
       }
   }

   public Transform PlayerTransform{
       get{
           return _player.GetComponent<Transform>();
       }
   }
   
   private int _currentLifePoint = 0;
   public int CurrentLifePoint{
       get{
           return _currentLifePoint;
       }
   }

    
    public void Awake()
    {
        // GameLoopManager.Instance.StartLoop_1 += Init;
    }
    
   public void Init() {
       if(Player == null){
           //If the player is not on the scene
        GameObject player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        Player = player.GetComponent<PlayerControlleur>();
        _currentLifePoint = Player.LifePoint;
       }
   }
}
