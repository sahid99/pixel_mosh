using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Context of the state
public class Player : MonoBehaviour
{
    public float Health;
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Right;
    public KeyCode Left;
    public KeyCode GrabDrop;
    public KeyCode Shoot;
    public float MoveSpeed;
    public Transform Hand;
    public Transform CharacerSprite;
    public GameObject Weapon;
    public GameObject SelectedObj; 
    private Vector2 _Aim;
    // Start is called before the first frame update
    private PlayerState _State;
    public Queue<float> Effect;
    void Start()
    {
        _State = new PlayerAlive(this);
        Effect = new Queue<float>();
    }
    // Update is called once per frame
    void Update()
    {
        _State.UpdateState();
    }
    void FixedUpdate(){
        _State.FixedUpdateState();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        if(obj == Weapon){ //If we are triggering with our weapon
            SelectedObj = null;
            return;
        }
        Weapon weapon = obj.GetComponent<Weapon>();
        if(weapon){
            SelectedObj = obj;
        }
        // Debug.Log("Enter");

    }
    void OnTriggerExit2D(Collider2D col)
    {
        SelectedObj = null;
        // Debug.Log("Exit");
    }    

    public Vector2 Aim{
        get{return _Aim;}
    }

}
