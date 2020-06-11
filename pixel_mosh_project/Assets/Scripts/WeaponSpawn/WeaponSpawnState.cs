using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSpawnState
{
    protected WeaponSpawner spawner;
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
}
