using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    public Player Player{
        set{player = value;}
        get{return player;}
    }
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
}
