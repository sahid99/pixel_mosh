using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapon : WeaponState
{
    public DropWeapon(Weapon weapon){
        this.weapon = weapon;
        foreach(var i in this.weapon.BulletPool){
            if(i.activeInHierarchy){
                //Is going to be destroyed on collision
                i.GetComponent<Bullet>().IsOnWeapon = false;
            }
            else{
                //Destroy the not on air intance
                weapon.DestroyObj(i);
            }
        }
        weapon.BulletPool = new List<GameObject>(); //Clean the pool
    }
    public override void UpdateState(){
        if(weapon.Ammo <= 0){
            weapon.DestroyObj(weapon.gameObject);
        }
    }
    public override void FixedUpdateState(){

    }
}
