using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float SpawnDelay;
    public GameObject CurrentWeapon;
    public List<GameObject> Weapons;
    private WeaponSpawnState _State;
    void Start()
    {
        // Weapons = new List<GameObject>();
        _State = new WeaponInGame(this);
    }

    // Update is called once per frame
    void Update()
    {
        _State.UpdateState();
    }
    public WeaponSpawnState State{
        set{_State = value;}
    }
    public GameObject CreateObj(GameObject obj){
        return Instantiate(obj);
    }
}
