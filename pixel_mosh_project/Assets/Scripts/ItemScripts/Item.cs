using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

    protected float _EffectValue;
    public abstract void SetEffect(float value);
    public abstract float ApplyEffect();
    public abstract void EffectApplied(); 
}
