using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Vector2 Velocity;
    [SerializeField]
    public float MoveSpeed;
    public float MaxVelocity;
    public float RayOffset;
    public float RayDistance;
    public Transform UpSight;
    public Transform DownSight;
    public Transform RigthSight;
    public Transform LeftSight;
    public Vector2 Acceleration;
    private Rigidbody2D _rb;
    public GameObject Manager;
    private BotManager _Manager;
    void OnEnable(){
        Velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        // Velocity = Vector2.up;
        Acceleration = Vector2.zero;
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _Manager = Manager.GetComponent<BotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Acceleration += GetCohesion();
        Acceleration += GetSeparation();
        // Acceleration += GetAvoidance();
        Acceleration += GetAlignment();

        Velocity += Acceleration;
        Velocity = Vector2.ClampMagnitude(Velocity, MaxVelocity);
        Acceleration = Vector2.zero;
    }
    void FixedUpdate(){
         _rb.MovePosition(_rb.position + Velocity*Time.fixedDeltaTime*MoveSpeed);
          
        // Debug.Log(start);
        
    }
    Vector2 GetAvoidance(){

        Vector2 total = Vector2.zero;
        //Up
        RaycastHit2D hitUp = Physics2D.Raycast(UpSight.position, Vector2.up, RayDistance);
        if(hitUp.collider != null){
            Vector2 invert = -Vector2.up;
            invert.Normalize();
            total+=invert;
            Debug.Log(hitUp.collider);
        }
        //Down
        RaycastHit2D hitDown = Physics2D.Raycast(DownSight.position, -Vector2.up, RayDistance);
        if(hitDown.collider != null){
            Vector2 invert = Vector2.up;
            invert.Normalize();
            total+=invert;
            Debug.Log(hitDown.collider);
        }
        //Right
        RaycastHit2D hitRight = Physics2D.Raycast(DownSight.position, Vector2.right, RayDistance);
        if(hitRight.collider != null){
            Vector2 invert = -Vector2.right;
            invert.Normalize();
            total+=invert;
            Debug.Log(hitRight.collider);
        }
        //Left
        RaycastHit2D hitLeft = Physics2D.Raycast(DownSight.position, -Vector2.right, RayDistance);
        if(hitLeft.collider != null){
            Vector2 invert = Vector2.right;
            invert.Normalize();
            total+=invert;
            Debug.Log(hitLeft.collider);
        }

        total.Normalize();
        total *= 5f;
        total -= Velocity;
        total = Vector2.ClampMagnitude(total, _Manager.MaxForce);
        return total*_Manager.AvoidanceWeight;
    }
    Vector2 GetSeparation(){
        if(_Manager.BotPool.Count == 0) return Vector2.zero;
        Vector2 total = Vector2.zero;
        int count = 0;
        Vector2 botPos = new Vector2(
            transform.position.x,
            transform.position.y);
        for(int i=0; i<_Manager.BotPool.Count; i++){
            Vector2 nextBotPos = new Vector2(
                _Manager.BotPool[i].gameObject.transform.position.x,
                _Manager.BotPool[i].gameObject.transform.position.y
            );
            float distace = Vector2.Distance(botPos, nextBotPos);
            if( distace>0 && distace < _Manager.SeparationDistance){
                Vector2 invert = botPos - nextBotPos;
                invert.Normalize();
                invert = invert/distace;
                total += invert;
                count++;
            }
        }
        if(count > 0){
            total = total/count;
            total.Normalize();
            total *= 5f;
            total -= Velocity;
            total = Vector2.ClampMagnitude(total, _Manager.MaxForce);
        }
        return total*_Manager.SeparationWeight;
    }
    Vector2 GetAlignment(){
        if(_Manager.BotPool.Count == 0) return Vector2.zero;
        Vector2 total = Vector2.zero;
        int count = 0;
        Vector2 botPos = new Vector2(
            transform.position.x,
            transform.position.y);
        for(int i=0; i<_Manager.BotPool.Count; i++){
            Vector2 nextBotPos = new Vector2(
                _Manager.BotPool[i].gameObject.transform.position.x,
                _Manager.BotPool[i].gameObject.transform.position.y
            );
            float distace = Vector2.Distance(botPos, nextBotPos);
            if( distace>0 && distace < _Manager.AlignmentRadious){
                total += nextBotPos;
                count++;
            }
        }
        if(count > 0){
            total = total/count;
            total.Normalize();
            total *= 5f;
            total -= Velocity;
            total = Vector2.ClampMagnitude(total, _Manager.MaxForce);
        }
        return total*_Manager.AlignmentWeight;
    }
    Vector2 GetCohesion(){
        if(_Manager.BotPool.Count == 0) return Vector2.zero;
        Vector2 total = Vector2.zero;
        int count = 0;
        Vector2 botPos = new Vector2(
            transform.position.x,
            transform.position.y);
        for(int i=0; i<_Manager.BotPool.Count; i++){
            Vector2 nextBotPos = new Vector2(
                _Manager.BotPool[i].gameObject.transform.position.x,
                _Manager.BotPool[i].gameObject.transform.position.y
            );
            float distace = Vector2.Distance(botPos, nextBotPos);
            if( distace>0 && distace < _Manager.CohesionRadious){
                Vector2 invert = nextBotPos - botPos;
                invert.Normalize();
                invert = invert/distace;
                total += invert;
                count++;
            }
        }
        if(count > 0){
            total = total/count;
            total.Normalize();
            total *= 5f;
            total -= Velocity;
            total = Vector2.ClampMagnitude(total, _Manager.MaxForce);
        }
        return total*_Manager.CohesionWeight;
    }
}
