using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    #region Attribut
    [SerializeField] private Transform[] _spawnPoints = null;
    [SerializeField] GameObject[] _mobToSpawns = null;
    [SerializeField] private float _timeBeforeSpawn = 1f;
    private float _cooldownTimer = 0f;

    #if UNITY_EDITOR 
    [ReadOnly]
    #endif 
    [SerializeField] private List<Monster> _mobAlive = new List<Monster>();
    public List<Monster> MobAlive{
        get{
            return _mobAlive;
        }
        set{
            _mobAlive = value;
        }
    }

    #endregion



    
    public void GameLoop()
    {
        _cooldownTimer += Time.deltaTime;
        if(_cooldownTimer >= _timeBeforeSpawn){
            Spawn();
            _cooldownTimer = 0f;
        }
    }

    void Spawn(){

        Transform closestPoint = null;
        float distanceMin = float.MaxValue;
        Transform player = PlayerManager.Instance.PlayerTransform;

        foreach(var spawnPoint in _spawnPoints){
            if((player.position - spawnPoint.position).magnitude < distanceMin){
                distanceMin = (player.position - spawnPoint.position).magnitude;
                closestPoint = spawnPoint;
            }
        }
        GameObject tmp_monster = Instantiate(_mobToSpawns[Random.Range(0, _mobToSpawns.Length)], closestPoint.position, closestPoint.rotation);
        _mobAlive.Add(tmp_monster.GetComponent<Monster>());
    }

    public void RemoveAEntry(Monster entry){
        foreach (var mob in MobAlive)
        {
            if(mob.Equals(entry)){
                MobAlive.Remove(mob);
            }
            return;
        }
    }

    public void KillAllMob(){
        foreach (var mob in MobAlive)
        {
            if(mob != null){
                GameLoopManager.Instance.GameLoop -= mob.GameLoop;
                Destroy(mob.gameObject);
            }
        }
        MobAlive = new List<Monster>();
    }
}
