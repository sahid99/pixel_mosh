using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInGame : WeaponSpawnState
{
    private Transform _SpawnerReference;
    private Transform _WeaponReference;
    public WeaponInGame(WeaponSpawner spawner){
        this.spawner = spawner;
        if(spawner.Weapons.Count > 0){
            int randomIndex = Random.Range(0, spawner.Weapons.Count);
            GameObject weapon = spawner.CreateObj(spawner.Weapons[randomIndex]);
            weapon.transform.position = spawner.transform.position;
            weapon.transform.rotation = spawner.transform.rotation;
            weapon.transform.parent = spawner.transform;
            spawner.CurrentWeapon = weapon;
            _SpawnerReference = spawner.transform;
            _WeaponReference = weapon.transform;
        }
    }
    public override void UpdateState(){
        if(_WeaponReference.parent != _SpawnerReference){
            spawner.State = new WeaponSpawnDelay(spawner);
        }
    }
    public override void FixedUpdateState(){

    }
}
