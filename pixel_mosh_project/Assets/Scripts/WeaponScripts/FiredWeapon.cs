using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredWeapon : WeaponState
{
    private float _Delay;
    public FiredWeapon(Weapon weapon){
        this.weapon = weapon;
        _Delay = 0f;
    }

    public override void UpdateState(){
        if(_Delay > weapon.FireRate){
            weapon.State = new LoadedWeapon(weapon);
        }
        _Delay += Time.deltaTime;
    }
    public override void FixedUpdateState(){

    }
}
