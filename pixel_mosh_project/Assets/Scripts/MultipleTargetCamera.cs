using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> Targets;
    public Vector3 Offset;
    public float SmoothFactor;
    private Vector3 _Velocity;

    void LateUpdate(){
        Vector3 center = GetCenterTargets();
        Vector3 cameraPos = center + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref _Velocity, SmoothFactor);
    }
    Vector3 GetCenterTargets(){
        if(Targets.Count == 1){
            return Targets[0].position;
        }
        var bounds = new Bounds(Targets[0].position, Vector3.zero);
        for(int i=0; i<Targets.Count; i++){
            bounds.Encapsulate(Targets[i].position);
        }
        return bounds.center;
    }
}
