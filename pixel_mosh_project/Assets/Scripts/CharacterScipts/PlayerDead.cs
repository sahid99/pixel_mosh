using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : PlayerState
{
    private float _DeadTime;
    private float _TimeCount;
    public PlayerDead(Player player){
        this.player = player;
        _DeadTime = 3.5f;
        _TimeCount = 0;
        this.player.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
    public override void UpdateState(){
        if(_TimeCount > _DeadTime){
            player.transform.position = player.RespawnPoint;
            if (TDMManager.sceneIndex == 1 || TDMManager.sceneIndex == 2){
                if(player.PlayerNo == 1){
                    TDMManager.rScore += 1;
                } else {
                    TDMManager.bScore += 1;
                }
            }
            player.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            player.State = new PlayerImmune(player);
        }
        _TimeCount += Time.deltaTime;
    }
    public override void FixedUpdateState(){

    }
}
