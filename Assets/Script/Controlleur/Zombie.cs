using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster
{
    #region variable
    [SerializeField] private int _dammage = 1;
    public int Dammage{
        get{
            return _dammage;
        }

    }
    
    [SerializeField] protected ParticleSystem _deathFX = null;


    #endregion
    public override void Building(){
        if(_cooldownBuildingTime < _buildingTime){
            _cooldownBuildingTime += Time.deltaTime;
            _buildingLerp = _cooldownBuildingTime / _buildingTime;
            _spriteRenderer.color = Color.Lerp(Color.black, Color.white, _buildingLerp);
        }else{
            _stateMachine.ChangeState(new ChasingState(this));
        }
    }

    public override void Hunting(){
        Vector3 playerPos = PlayerManager.Instance.PlayerTransform.position;
        Vector2 direction = (playerPos - transform.position).normalized * Speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + direction);
        LookAtPlayer();
    }

    public override void Dead(){
        base.Dead();
        Instantiate(_deathFX, transform.position, Quaternion.identity);
        _spriteRenderer.color = Color.black;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _animator.SetTrigger("Dead");
        gameObject.layer = 0;
        _isAlive = false;
        Destroy(gameObject, _timeBeforeDestroy);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(_isAlive){
            switch(other.gameObject.layer){
                case 9: //Player
                    other.gameObject.GetComponent<PlayerControlleur>().LooseLifePoint(Dammage);
                break;
            }
        }
    }
}
