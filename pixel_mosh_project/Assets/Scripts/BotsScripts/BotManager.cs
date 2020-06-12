using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxPoolSize;
    public GameObject BotPrefab;
    public List<Bot> BotPool;
    public float SeparationDistance;
    public float CohesionRadious;
    public float AlignmentRadious;
    public float MaxForce;
    public float SeparationWeight;
    public float CohesionWeight;
    public float AlignmentWeight;
    public float AvoidanceWeight;
    void Start()
    {
        for(int i=0; i<MaxPoolSize; i++){
            GameObject tempBot = Instantiate(BotPrefab);
            Bot tempBotScript = tempBot.GetComponent<Bot>();
            tempBotScript.Manager = this.gameObject;
            BotPool.Add(tempBotScript);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
