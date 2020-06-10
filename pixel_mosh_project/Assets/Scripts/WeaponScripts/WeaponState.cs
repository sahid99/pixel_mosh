using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponState
{
    protected Weapon weapon;
    protected WeaponState PrevState;

    public abstract void UpdateState();
    public abstract void FixedUpdateState();
}
