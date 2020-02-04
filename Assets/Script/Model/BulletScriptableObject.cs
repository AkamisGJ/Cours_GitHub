using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Bullet", menuName ="ScriptableObjects/Bullet", order = 2)]
public class BulletScriptableObject : ScriptableObject
{
    public string _bulletName = "Bullet Name";
    public Sprite _sprite = null;
    public int _dammage = 1;
    public float _speed = 20f;
    
}
