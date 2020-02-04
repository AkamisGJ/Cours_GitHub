using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]

/*
This is the parent class for all ennemys
*/
public class Monster : MonoBehaviour
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
    [SerializeField] private int _lifePoint = 3;
    public int LifePoint{
        get{
            return _lifePoint;
        }
        set{
            _lifePoint = value;
            
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
    
    [SerializeField] protected float _buildingTime = 2f;
    [SerializeField] protected float _timeBeforeDestroy = 5f;
    [SerializeField] protected int _score = 50;
    protected float _cooldownBuildingTime = 0f;
    protected float _buildingLerp = 0f;
    protected bool _isAlive = true;
    protected Rigidbody2D _rigidbody = null;
    protected SpriteRenderer _spriteRenderer = null;
    protected Animator _animator = null;
    protected StateMachine _stateMachine = new StateMachine();

    #endregion

   
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if(_rigidbody == null)
            Debug.LogError(gameObject.name + " do not have a Rigibody ");

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if(_spriteRenderer == null)
            Debug.LogError(gameObject.name + " do not have a Sprite Renderer ");

        _animator = GetComponentInChildren<Animator>();
        if(_animator == null)
            Debug.LogError(gameObject.name + " do not have a Animator ");

    }

    private void Start() {
        _stateMachine.ChangeState(new BuildingState(this));
        GameLoopManager.Instance.GameLoop += GameLoop;
    }



    public void GameLoop()
    {
        _stateMachine.Update();
    }


    public virtual void Building(){
        //Override in the child class
        print(gameObject.name + "Do not override the Building function");
    }

    public virtual void Hunting(){
        //Override in the child class
        print(gameObject.name + "Do not override the Building function");
    }

    public virtual void Dead(){
        GameManager.Instance.ChangeUIScore(_score);
        SpawnerManager.Instance.RemoveAEntry(this);
    }



    public void LoseLife(int dammage){
        LifePoint -= dammage;
        // _onPvChange();
        if(LifePoint <= 0){
            _stateMachine.ChangeState(new StateDead(this));
        }
    }

    protected void LookAtPlayer(){
        transform.up = PlayerManager.Instance.PlayerTransform.position - transform.position;
    }
}
