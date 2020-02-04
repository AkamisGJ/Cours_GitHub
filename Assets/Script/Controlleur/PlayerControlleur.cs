using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControlleur : MonoBehaviour
{
    #region Attribut
    [SerializeField] private float _speed = 10f;
    public float Speed{
        get{
            return _speed;
        }
        set{
            _speed = value;
        }
    }
    private Action _onPvChange = null;
    public event Action OnPvChange{
        add{
            _onPvChange -= value;
            _onPvChange += value;
        }remove{
            _onPvChange -= value;
        }
    }
    [SerializeField] private int _lifePoint = 3;
    public int LifePoint{
        get{
            return _lifePoint;
        }
        set{
            _lifePoint = value;
            
        }
    }
    [SerializeField] private GameObject _bullet = null;
    [SerializeField] private Transform _shootpoint = null;

    private Rigidbody2D _rigidbody = null;
    private StateMachine _stateMachine = new StateMachine();

    #endregion

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        if(PlayerManager.Instance.Player == null){
            PlayerManager.Instance.Player = this;
        }
    }

    private void Start()
    {
        _stateMachine.ChangeState(new AliveState(this));
        InputManager.Instance.OnKeyChange += Movement;
        InputManager.Instance.Fire += Shooting;
        GameLoopManager.Instance.GameLoop += GameLoop;
        OnPvChange += GameManager.Instance.ChangeUILifePoint;
    }

    public void GameLoop() {
        _stateMachine.Update();
    }

    public void LooseLifePoint(int value){
        if(LifePoint > 0){
            LifePoint -= value;

            if(_onPvChange != null){
                _onPvChange(); //Change LifePoint in UI
            }

            if(LifePoint <= 0){
                _stateMachine.ChangeState(new DeadPlayerState(this));
            }
        }
    }

    private void Movement(){
        Vector2 direction = InputManager.Instance.Direction * Speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + direction);
    }

    private void Shooting(){
        GameObject tmp_bullet = Instantiate(_bullet, _shootpoint.position, transform.rotation);
    }

    public void Aiming(){
        transform.LookAt(InputManager.Instance.MousePosition, Vector3.forward);
    }

    public void Dead(){
        GameLoopManager.Instance.GameLoop -= GameLoop;
        InputManager.Instance.OnKeyChange -= Movement;
        InputManager.Instance.Fire -= Shooting;
    }

    public void Restart(){
        GameLoopManager.Instance.GameLoop += GameLoop;
        InputManager.Instance.OnKeyChange += Movement;
        InputManager.Instance.Fire += Shooting;
        LifePoint = 3;
        transform.position = Vector2.zero;
        _stateMachine.ChangeState(new AliveState(this));
    }
}
