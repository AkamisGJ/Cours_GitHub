using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private BulletScriptableObject _bulletData = null;
   public BulletScriptableObject BulletData{
       get{
           return _bulletData;
       }
       set{
           _bulletData = value;
       }
   }
    private SpriteRenderer _spriteRenderer = null;

    public int Dammage{
        get{
            return _bulletData._dammage;
        }
    }

    private void Awake() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    private void Start() {
        gameObject.name = _bulletData._bulletName;
        _spriteRenderer.sprite = _bulletData._sprite;
        GetComponent<Rigidbody2D>().AddForce(transform.up * _bulletData._speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.layer){
            case 10: //Monster
                other.gameObject.GetComponent<Monster>().LoseLife(Dammage);
                Destroy(gameObject);
            break;

            case 9: //Player
                other.gameObject.GetComponent<PlayerControlleur>().LooseLifePoint(Dammage);
                Destroy(gameObject);
            break;

            default :
                Destroy(gameObject);
            break;
        }   
    }
}
