using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlive : PlayerState
{
    private Rigidbody2D _rb;
    private Vector2 _NextMove;
    private Weapon _CurrentWeapon;
    private Vector2 _Aim;
    public PlayerAlive(Player player){
        this.player = player;
        _rb = player.GetComponent<Rigidbody2D>();
        _Aim = Vector2.zero;
    }
    public void ApplyHealth(){
        while(player.Effect.Count > 0){
            player.Health += player.Effect.Dequeue();
        }
    }
    void ShootWeapon(){
        if(_CurrentWeapon){
            _CurrentWeapon.FireButton = 
            _CurrentWeapon.IsAutomatic 
            ? Input.GetKey(player.Shoot) 
            : Input.GetKeyDown(player.Shoot);  
        }
    }
    Vector2 GetMoveInputs(){
        Vector2 move = Vector2.zero;
        if(Input.GetKey(player.Up)) move.y+=1;
        if(Input.GetKey(player.Down)) move.y-=1;
        if(Input.GetKey(player.Right)) move.x+=1;
        if(Input.GetKey(player.Left)) move.x-=1;
        
        return move; 
    }
    void IntereactWeapon(){
        if(Input.GetKeyDown(player.GrabDrop)){
            // Debug.Log("GotInput");
            if(player.Weapon){ //We have a weapon
                //Drop
                Debug.Log("Drop");
                player.GetComponent<AudioSource>().Play();
                WeaponDrop();
            }
            else if(!player.SelectedObj){
                // Debug.Log("Not Selected");
                return;
            }
            else if(player.SelectedObj){
                //Grab
                // Debug.Log("Grab");
                player.Weapon = player.SelectedObj;
                player.GetComponent<AudioSource>().Play();
                SetUpWeapon();
            }
        }
    }
    void SetUpWeapon(){
        player.Weapon.transform.position = player.Hand.position;
        player.Weapon.transform.rotation = player.Hand.rotation;
        player.Weapon.transform.parent = player.Hand;
        Weapon weapon = player.Weapon.GetComponent<Weapon>();
        weapon.PlayerHolding = player.gameObject;
        weapon.State = new ActivateWeapon(weapon);
        _CurrentWeapon = weapon;
    }
    void WeaponDrop(){
        player.Weapon.transform.parent = null;
        Weapon weapon = player.Weapon.GetComponent<Weapon>();
        weapon.State = new DropWeapon(weapon);
        player.Weapon = null;
        _CurrentWeapon = null;
    }
    void AimGun(){
        Vector2 aiming = Vector2.zero;
        if(Input.GetKey(player.Up)) aiming.y+=1;
        if(Input.GetKey(player.Down)) aiming.y-=1;
        if(Input.GetKey(player.Right)) aiming.x+=1;
        if(Input.GetKey(player.Left)) aiming.x-=1;
        //Check for updates in movement
        if(aiming != Vector2.zero){
            _Aim = aiming;
        }

        float angle = Mathf.Atan2(_Aim.y, _Aim.x) * Mathf.Rad2Deg;
        int cuadrante = Cuadrante(angle);
        switch(cuadrante){
            case 1:
                player.CharacerSprite.eulerAngles = Vector3.up*0;
                player.Hand.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            break;
            case 2:
                player.CharacerSprite.eulerAngles = Vector3.up*180;
                player.Hand.transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
            break;
            case 3:
                player.CharacerSprite.eulerAngles = Vector3.up*180;
                player.Hand.transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
            break;
            case 4:
                player.CharacerSprite.eulerAngles = Vector3.up*0;
                player.Hand.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            break;
        }
    }
    int Cuadrante(float angle){
        if(angle >= 0 && angle < 90) return 1;
        if(angle >= 90 && angle <= 180) return 2;
        if(angle > -180 && angle < -90) return 3;
        if(angle >= -90 && angle < 0) return 4;
        return -1;
    }
    public override void UpdateState(){
        if(player.Health <= 0){
            if(player.Weapon){
                WeaponDrop();
            }
            player.State = new PlayerDead(player);
        }
        ApplyHealth();
        IntereactWeapon();
        AimGun();
        ShootWeapon();
        _NextMove = GetMoveInputs();
    }
    public override void FixedUpdateState(){
        _rb.MovePosition(_rb.position + _NextMove*Time.fixedDeltaTime*player.MoveSpeed);
    }

}
