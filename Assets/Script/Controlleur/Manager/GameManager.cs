using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Doozy.Engine;

public class GameManager : Singleton<GameManager>
{
   [SerializeField] private TextMeshProUGUI _lifePoint = null;
   [SerializeField] private TextMeshProUGUI _scoreText = null;
   [SerializeField] private TextMeshProUGUI _finalScoreText = null;
   private int _score = 0;
   private bool _pause = false;

    void Awake(){
        InputManager.Instance.Pause += PauseGame;
    }

   void Init()
   {
       ChangeUILifePoint();
       ChangeUIScore(_score);
   }

   public void ChangeUILifePoint(){
       _lifePoint.text = "LifePoint : " + PlayerManager.Instance.Player.LifePoint;

       if(PlayerManager.Instance.Player.LifePoint <= 0){
           GameEventMessage.SendEvent("EndGame");
           GameLoopManager.Instance.GameLoop -= SpawnerManager.Instance.GameLoop;
            GameLoopManager.Instance.GameLoop -= CameraFollowPlayer.Instance.GameLoop;
       }
   }

   public void ChangeUIScore(int newScore){
       _score += newScore;
       _scoreText.text = "Score : " + _score;
       _finalScoreText.text = "Final Score : " + _score;
   }

   public void PauseGame(){
       if(_pause == false){
        GameEventMessage.SendEvent("Pause");
            //Pause InputSystem
            GameLoopManager.Instance.GameLoop -= InputManager.Instance.GameLoop;
            //Pause all the mob
            GameLoopManager.Instance.GameLoop -= SpawnerManager.Instance.GameLoop;
            foreach (var mob in SpawnerManager.Instance.MobAlive)
            {
                GameLoopManager.Instance.GameLoop -= mob.GameLoop;
            }
            //Pause the player
            GameLoopManager.Instance.GameLoop -= PlayerManager.Instance.Player.GameLoop;
            _pause = true;
       }else{
            //Pause InputSystem
            GameLoopManager.Instance.GameLoop += InputManager.Instance.GameLoop;
            //Pause all the mob
            GameLoopManager.Instance.GameLoop += SpawnerManager.Instance.GameLoop;
            foreach (var mob in SpawnerManager.Instance.MobAlive)
            {
                GameLoopManager.Instance.GameLoop += mob.GameLoop;
            }
            //Pause the player
            GameLoopManager.Instance.GameLoop += PlayerManager.Instance.Player.GameLoop;
            _pause = false;
       }
   }

   public void RestartGame(){
       _score = 0;
       GameLoopManager.Instance.GameLoop += SpawnerManager.Instance.GameLoop;
       GameLoopManager.Instance.GameLoop += CameraFollowPlayer.Instance.GameLoop;
       PlayerManager.Instance.Init();
       PlayerManager.Instance.Player.Restart();
       Init();
       ChangeUILifePoint();
       ChangeUIScore(_score);
       SpawnerManager.Instance.KillAllMob();
   }
}
