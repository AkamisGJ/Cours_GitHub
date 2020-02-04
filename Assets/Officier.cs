using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Officier : Monster
{
    [SerializeField] private float _distanceToShoot = 20f;
    [SerializeField] private float _cooldownBetweenShoot = 2f;
    private float _tmpCooldownShoot = 0f;
    [SerializeField] private Bullet _bullet = null;
    [SerializeField] private Transform _shootPoint = null;


    public override void Building(){
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        if(_cooldownBuildingTime < _buildingTime){
            _cooldownBuildingTime += Time.deltaTime;
            _buildingLerp = _cooldownBuildingTime / _buildingTime;
            _spriteRenderer.color = Color.Lerp(Color.black, Color.white, _buildingLerp);
        }else{
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _stateMachine.ChangeState(new ChasingState(this));
        }
    }

    public override void Hunting(){
        Vector3 playerPos = PlayerManager.Instance.PlayerTransform.position;
        Vector2 direction = (playerPos - transform.position).normalized * Speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + direction);
        LookAtPlayer();

        float distance =  (playerPos - transform.position).magnitude;
        _tmpCooldownShoot += Time.deltaTime;

        if(distance < _distanceToShoot && _tmpCooldownShoot > _cooldownBetweenShoot){
            Shoot();
        }
    }

    public override void Dead(){
        base.Dead();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        int rng = 0;
        while(rng == 0){
            rng = Random.Range(-1, 2);
        }
        _animator.SetInteger("Random", rng);
        _animator.SetTrigger("Dead");

        gameObject.layer = 0;
        _isAlive = false;
        Destroy(gameObject, _timeBeforeDestroy);
    }

    private void Shoot(){
        if(_bullet == null){
            Debug.LogError(gameObject.name + " don't have a bullet reference in prefab");
        }

        Bullet bullet = Instantiate(_bullet, _shootPoint.position, transform.rotation);
        _tmpCooldownShoot = 0f;
    }
}
