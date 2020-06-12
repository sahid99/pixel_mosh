using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public Vector3 RespawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.name == "Player1" || col.name == "Player2"){
            Debug.Log("Choque con" + col.name);
            this.transform.parent = col.gameObject.transform;
        }
        if (col.name == "RedBase"){
            TDMManager.rScore +=1;
            this.transform.position = RespawnPoint;
            this.transform.parent = null;
        } else if (col.name == "BlueBase"){
            TDMManager.bScore +=1;
            this.transform.position = RespawnPoint;
            this.transform.parent = null;
        }
    }
}
