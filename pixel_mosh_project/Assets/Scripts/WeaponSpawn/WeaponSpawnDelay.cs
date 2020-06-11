using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnDelay : WeaponSpawnState
{
    private float _MaxDelay;
    private float _Delay;
    public WeaponSpawnDelay(WeaponSpawner spawner){
        this.spawner = spawner;
        _Delay = 0f;
        _MaxDelay = spawner.SpawnDelay;
    }
    public override void UpdateState(){
        if(_Delay > _MaxDelay){
            spawner.State = new WeaponInGame(spawner);
        }
        _Delay+=Time.deltaTime;
    }
    public override void FixedUpdateState(){
        
    }
}
