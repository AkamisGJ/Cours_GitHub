using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : Singleton<CameraFollowPlayer>
{
    private Transform _player;
    private Camera _camera;
    [SerializeField] [Range(0f, 10f)] private float _lerpSpeed = 0.2f;
    [SerializeField] [Range(1f, 20f)] private float _distance = 5f;

   
    void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    
    public void GameLoop()
    {
        if(_player == null){
             _player = PlayerManager.Instance.PlayerTransform;
        }else{
            Vector3 newPosition = _player.position;
            newPosition.z = transform.position.z;
            _camera.orthographicSize = _distance;
            transform.position = Vector3.Lerp(transform.position, newPosition, _lerpSpeed * Time.deltaTime);
        }
    }
}
