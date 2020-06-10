using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public Transform BallOrigin;
    public GameObject BallPrefab;
    public float Force;
    // public Inpu FireButton;
    void Start()
    {
        // Debug.Log(Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        if(Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }
    void Shoot(){
        GameObject tmpBall = Instantiate(BallPrefab, BallOrigin.position, BallOrigin.rotation);
        tmpBall.GetComponent<Bullet>().SetEffect(-2f);
        tmpBall.GetComponent<Bullet>().Player = Player;
        Rigidbody2D tmpBallRigidbody = tmpBall.GetComponent<Rigidbody2D>();
        if(tmpBallRigidbody != null){
            tmpBallRigidbody.AddForce(transform.right*Force, ForceMode2D.Impulse);
        }
    }
}
