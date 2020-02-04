using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
The Event name use the default name of the InputManager of Unity
*/

public class InputManager : Singleton<InputManager>
{
    #region Attribut
    private Action _onKeyChange = null;
    public event Action OnKeyChange{
        add{
            _onKeyChange -= value;
            _onKeyChange += value;
        }
        remove{
            _onKeyChange -= value;
        }
    }

    //Direction use the ZSQD and arrow key
    private Vector3 _direction = Vector3.zero;
    public Vector3 Direction{
        get{
            return _direction;
        }
        set{
            _direction = value;
        }
    }

    //Track the position of the Mouse
    [SerializeField] private Camera _camera = null;
    private Vector3 _mousePosition = Vector3.zero;
    public Vector3 MousePosition{
        get{
            return _mousePosition;
        }
        set{
            _mousePosition = value;
        }
    }

    private Action _fire = null;
    public event Action Fire{
        add{
            _fire -= value;
            _fire += value;
        }
        remove{
            _fire -= value;
        }
    }

    private Action _pause = null;
    public event Action Pause{
        add{
            _pause -= value;
            _pause += value;
        }
        remove{
            _pause -= value;
        }
    }

    #endregion
    void Start()
    {
        if(_camera == null){
            _camera = Camera.main;
        }
        GameLoopManager.Instance.GameLoop += GameLoop;
        GameLoopManager.Instance.ManagerLoop += PausePress;
    }

    // Update is called once per frame
    public void GameLoop(){
        CalculateDirection();
        CalculateMousePosition();
        
        if(Input.anyKey && _onKeyChange != null){
            _onKeyChange();
        }

        if(Input.GetButtonDown("Fire1") && _fire != null){
            _fire();
        }
    }

    void PausePress(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            _pause();
        }
    }

    void CalculateDirection(){
        Vector3 horizontalAxis = new Vector3 (Input.GetAxis("Horizontal"), 0f, 0f);
        Vector3 verticalAxis = new Vector3 (0f, Input.GetAxis("Vertical"), 0f);

        Direction = horizontalAxis + verticalAxis;
    }

    void CalculateMousePosition(){
        MousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
