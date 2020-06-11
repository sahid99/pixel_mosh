using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> Targets;
    public Vector3 Offset;
    public float SmoothFactor;
    public float MinZoom;
    public float MaxZoom;
    public float ZoomLimiter;
    private Vector3 _Velocity;
    private Camera camera;

    void Start(){
        camera = GetComponent<Camera>();
    }
    void LateUpdate(){
        if(Targets.Count == 0){
            return;
        }
        MoveCamera();
        ZoomCamera();
    }
    void MoveCamera(){
        Vector3 center = GetCenterTargets();
        Vector3 cameraPos = center + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref _Velocity, SmoothFactor);
    }
    void ZoomCamera(){
        float zoom = Mathf.Lerp(MinZoom, MaxZoom, GetGreatestDistance()/ZoomLimiter);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoom, Time.deltaTime);
    }
    float GetGreatestDistance(){
        var bounds = new Bounds(Targets[0].position, Vector3.zero);
        for(int i=0; i<Targets.Count; i++){
            bounds.Encapsulate(Targets[i].position);
        }
        return bounds.size.x; //Widght
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
