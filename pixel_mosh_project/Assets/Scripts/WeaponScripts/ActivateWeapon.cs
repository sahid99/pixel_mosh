using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeapon : WeaponState 
{
    public ActivateWeapon(Weapon weapon){
        this.weapon = weapon;
        for(int i=0; i<weapon.MaxPool; i++){
            GameObject bullet = (GameObject)weapon.CreateObj(weapon.Bullet);
            Bullet _bullet = bullet.GetComponent<Bullet>();
            //Setting the damage bullet is going to deal
            if(_bullet){
                _bullet.SetEffect(weapon.Damage);
                //If the game is too slow check this, to use pointers
                _bullet.Player = weapon.PlayerHolding;
                _bullet.IsOnWeapon = true;
            }
            bullet.SetActive(false);
            weapon.BulletPool.Add(bullet);
        }
    } 
    public override void UpdateState(){
        weapon.State = new LoadedWeapon(weapon);
    }
    public override void FixedUpdateState(){

    }
}
