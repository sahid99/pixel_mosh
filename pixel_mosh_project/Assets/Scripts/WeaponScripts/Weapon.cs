using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerHolding; //The one who is holding the weapon 
    public List<GameObject> BulletPool;
    public int MaxPool;
    public GameObject Bullet;
    public Transform Barrel;
    public float FireRate;
    public bool IsAutomatic;
    public float Damage;
    public float BulletSpeed;
    public int Ammo;
    public bool FireButton;
    private WeaponState _State;
    void Start()
    {
        _State = new DropWeapon(this);
    }

    public WeaponState State{
        set{_State = value;}
    }

    // Update is called once per frame
    void Update()
    {
        // if(IsAutomatic){
        //     FireButton = Input.GetMouseButton(0);
        // }
        // else{
        //     FireButton = Input.GetMouseButtonDown(0);
        // }
        _State.UpdateState();
    }

    public GameObject CreateObj(GameObject obj){
        return Instantiate(obj);
    }
    public void DestroyObj(GameObject obj){
        Destroy(obj);
    }
}
