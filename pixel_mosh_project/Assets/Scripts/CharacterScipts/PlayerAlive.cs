using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlive : PlayerState
{
    private Rigidbody2D _rb;
    private Vector2 _NextMove;
    public PlayerAlive(Player player){
        this.player = player;
        _rb = player.GetComponent<Rigidbody2D>();
    }
    public void ApplyHealth(){
        while(player.Effect.Count > 0){
            player.Health += player.Effect.Dequeue();
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
            Debug.Log("GotInput");
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
        weapon.State = new ActivateWeapon(weapon);
    }
    void WeaponDrop(){
        player.Weapon.transform.parent = null;
        Weapon weapon = player.Weapon.GetComponent<Weapon>();
        weapon.State = new DropWeapon(weapon);
        player.Weapon = null;
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
        _NextMove = GetMoveInputs();
    }
    public override void FixedUpdateState(){
        _rb.MovePosition(_rb.position + _NextMove*Time.fixedDeltaTime*player.MoveSpeed);
    }

}
