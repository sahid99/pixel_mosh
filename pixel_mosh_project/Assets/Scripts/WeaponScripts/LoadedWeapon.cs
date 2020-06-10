using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadedWeapon : WeaponState
{
    public LoadedWeapon(Weapon weapon){
        this.weapon = weapon;
    }
    bool ToFireWeapon(){
        if(weapon.Ammo <= 0) return false; //Particle that is empty
        if(weapon.FireButton){
            return Shoot();
        }
        return false;  
    }
    bool Shoot(){
        for(int i = 0; i<weapon.BulletPool.Count; i++){
            if(!weapon.BulletPool[i].activeInHierarchy){
                Rigidbody2D bulletRB = weapon.BulletPool[i].GetComponent<Rigidbody2D>();
                if(bulletRB){
                    GameObject bullet = weapon.BulletPool[i];
                    bullet.transform.position = weapon.Barrel.position;
                    bullet.transform.rotation = weapon.Barrel.rotation; //Check
                    bullet.SetActive(true);
                    bulletRB.AddForce(weapon.transform.right*weapon.BulletSpeed, ForceMode2D.Impulse);
                    weapon.Ammo -= 1;
                    return true;
                }
            }
        }
        return false;
    }
    public override void UpdateState(){
        if(ToFireWeapon()){
            weapon.State = new FiredWeapon(weapon);
        }
    }
    public override void FixedUpdateState(){

    }
}
