using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Item
{
    //The player that shoots this bullet;
    private GameObject PlayerShooter;
    //This is to cheack if the bullet is going to be recycled
    //by a gun or not (destroy)
    public bool IsOnWeapon;
    public GameObject Player{
        set{PlayerShooter = value;}
    }
    public override void SetEffect(float value){
        this._EffectValue = value;
    }
    public override float ApplyEffect(){
        return this._EffectValue;
    }
    public override void EffectApplied(){
        if(IsOnWeapon){
            //If a weapon is using it recycle
            gameObject.SetActive(false);
        }
        else{
            //If not Destroy it
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //En teoría solo debe de funcionar si colisiona un personaje
        if(collision.gameObject == PlayerShooter) return;
        Player player = collision.gameObject.GetComponent<Player>();
        if(player) player.Effect.Enqueue(ApplyEffect());
        EffectApplied();
    }
}
