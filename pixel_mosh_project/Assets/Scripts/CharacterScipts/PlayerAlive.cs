using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlive : PlayerState
{
    private Rigidbody2D _rb;
    private Vector2 _NextMove;
    private Weapon _CarryWeapon;
    public PlayerAlive(Player player){
        this.player = player;
        _rb = player.GetComponent<Rigidbody2D>();
    }
    public void ApplyHealth(){
        while(player.Effect.Count > 0){
            player.Health += player.Effect.Dequeue();
        }
    }
    void ShootWeapon(){
        if(_CarryWeapon){
            _CarryWeapon.FireButton = player.Controlls.Player.Shoot.triggered;
        }
    }
    Vector2 GetMoveInputs(){
        // Vector2 move = Vector2.zero;
        // if(Input.GetKey(player.Up)) move.y+=1;
        // if(Input.GetKey(player.Down)) move.y-=1;
        // if(Input.GetKey(player.Right)) move.x+=1;
        // if(Input.GetKey(player.Left)) move.x-=1;
        Vector2 move = player.Controlls.Player.Move.ReadValue<Vector2>();
        return move; 
    }
    void IntereactWeapon(){
        // Debug.Log(player.Controlls.Player.GrabDrop.ReadValue<int>());
        if(player.Controlls.Player.GrabDrop.triggered){
        //     Debug.Log("GotInput");
            if(player.Weapon){ //We have a weapon
                //Drop
                Debug.Log("Drop");
                WeaponDrop();
            }
            else if(!player.SelectedObj){
                Debug.Log("Not Selected");
                return;
            }
            else if(player.SelectedObj){
                //Grab
                Debug.Log("Grab");
                player.Weapon = player.SelectedObj;
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
        _CarryWeapon = weapon;
    }
    void WeaponDrop(){
        player.Weapon.transform.parent = null;
        Weapon weapon = player.Weapon.GetComponent<Weapon>();
        weapon.State = new DropWeapon(weapon);
        player.Weapon = null;
        _CarryWeapon = null;
    }
    void AimGun(){
        float angle = Mathf.Atan2(player.Aim.y, player.Aim.x) * Mathf.Rad2Deg;
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
        if(angle >= 90 && angle < 180) return 2;
        if(angle >= -180 && angle < -90) return 3;
        if(angle >= -90 && angle < 0) return 4;
        return -1;
    }
    public override void UpdateState(){
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
