using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Monster", menuName ="ScriptableObjects/Monster", order = 1)]
public class Monster_ScriptableObject : ScriptableObject
{
    public string _monsterName = "Monster Name";
    public GameObject _gameObject = null;
    public int _lifePoint = 3;
    public int _dammage = 1;
    public float _speed = 6;
}
