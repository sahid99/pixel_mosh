using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoves : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Right;
    public KeyCode Left;
    public KeyCode DropTake;

    public Rigidbody2D rb;
    private Vector2 _Movement;
    public float MoveSpeed;
    public Transform HandGun;
    public Transform CharacerSprite;
    public GameObject NextGun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementKeys();
        // https://answers.unity.com/questions/791345/c-lookat-mouse-2d.html
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if((angle >= 0 && angle <= 90) || (angle > -90 && angle < 0)){
            CharacerSprite.eulerAngles = Vector3.up*0;
        }
        else{
            // Debug.Log(angle);
            CharacerSprite.eulerAngles = Vector3.up*180;
        }

        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < HandGun.transform.position.x){
            HandGun.transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
        }
        else{
            HandGun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }

        // Debug.Log(angle);
        // Vector3 eAngle = HandGun.eulerAngles;
        // // eAngle.y = CharacerSprite.eulerAngles.y; 
        // eAngle.x = CharacerSprite.eulerAngles.y;
        // eAngle.z = angle;
        // HandGun.eulerAngles = eAngle; 
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + _Movement*MoveSpeed*Time.fixedDeltaTime);
    }
    //  void OnCollisionEnter2D(Collision2D col)
    // {
    //     Debug.Log("OnCollisionEnter2D");
    //     Debug.Log(col.collider);
    // }
    void GetMovementKeys(){
        _Movement = Vector2.zero;

        if(Input.GetKey(Up)){
            _Movement.y += 1f;
        }
        if(Input.GetKey(Down)){
            _Movement.y += -1f;
        }

        if(Input.GetKey(Right)){
            _Movement.x += 1f;
        }
        if(Input.GetKey(Left)){
            _Movement.x += -1f;
        }
        if(Input.GetKeyDown(DropTake)){
            // _Movement.x += -1f;
        if(HandGun.childCount > 0){
            HandGun.GetChild(0).parent = null;
        }else if(NextGun)   {
            //Get reference to what is colliding
            NextGun.transform.parent = HandGun;
        }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        // spriteMove = -0.1f;
        NextGun = col.gameObject;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        NextGun = null;
    }
}
