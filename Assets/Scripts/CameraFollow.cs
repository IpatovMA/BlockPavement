using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float zoomSpeed = 5;
    public float offset = 0f;

    private Camera cam;
    private Transform CameraAlign;
    public MapData MapData;
    public float PlayCamHeight ;
    public float MenuCamHeight ;

    public float[] loopSpeed;
    public float changeLoopSpeedFactor;
    public float loopRadius;
    public float rotationStartRadius;
    public float viewAngel;
    public int fastLoopNum = 2;
    private Vector3 floatPos;
    // public Vector3[] MenuCamPos = new Vector3[2];
    private bool aligned=false;
    private float radius;
    private int loopCounter;
    private float currentLoopSpeed;


    // private bool RotateBool;// 
    void Start(){
        cam = GetComponent<Camera>();
        CameraAlign = transform.parent.transform;
        rotationStartRadius = 0.5f*loopRadius;
    }
    void FixedUpdate()
    {
         if (target == null) return;
            switch(LevelManager.State){
                case LevelManager.lvlState.Menu:
                        MapData = target.GetComponent<MapData>();
                        viewAngel = transform.eulerAngles.x;
                        loopCounter = 0;
                        if (MapData != null){  
                            // floatPos = new Vector3(MapData.MapWidth/2.0f,MapData.MapHeight/2.0f,0);
                            floatPos=Vector3.zero;
                            // transform.position =  new Vector3(0, -loopRadius, 0)+CameraAlign.position;
                            transform.localPosition =  new Vector3(0, -loopRadius, 0);

                            CameraAlign.position = target.position+floatPos+new Vector3(0,0,MenuCamHeight);
                            CameraAlign.eulerAngles = Vector3.zero;
                            loopRadius = MapData.MapHeight>MapData.MapWidth ? MapData.MapHeight : MapData.MapWidth;
                            rotationStartRadius = 0.5f*loopRadius;
                        }
                        ScaleWithFactor(16);
                        transform.LookAt(target.position+floatPos);
                break;
                case LevelManager.lvlState.Play:
                    transform.position = Vector3.Lerp(transform.position,CameraAlign.position, Time.deltaTime * zoomSpeed);
                    // LerpDovodchik(transform.position,CameraAlign.position);
                    HeightChange(PlayCamHeight);
                    ScaleWithFactor(13);
                    transform.LookAt(target.position+floatPos);
                break;
                case LevelManager.lvlState.Fin:
                    radius = Mathf.Lerp(radius, loopRadius,Time.deltaTime * zoomSpeed);
                    transform.localPosition  = Vector3.Lerp(transform.localPosition, new Vector3(0,-radius,0), Time.deltaTime * zoomSpeed);

                    HeightChange(MenuCamHeight);
                    // makeLoop(radius);
                    if (radius < rotationStartRadius){
                    transform.LookAt(target.position+floatPos);

                    }else{
                        if(loopCounter<fastLoopNum){
                            SmoothlChangeSpeed(0);
                        } else{
                            SmoothlChangeSpeed(1);
                        }

                        makeLoop(radius);

                    }
                    ScaleWithFactor(16);
                break;

            }

           
    }

    void ScaleWithFactor(float factor){
        int size = MapData.RotateOn%2==0?MapData.MapWidth:MapData.MapHeight;
        cam.fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, size*factor, Time.deltaTime * zoomSpeed); 
    }
    void HeightChange(float height){
        CameraAlign.position=Vector3.Lerp(CameraAlign.position,new Vector3(CameraAlign.position.x,CameraAlign.position.y,height), Time.deltaTime * zoomSpeed);
    }

    void makeLoop(float rad){
        float delta  = Time.deltaTime*currentLoopSpeed;
            if (transform.localEulerAngles.x<0.1f) transform.localEulerAngles = new Vector3(-delta,0,0);
       transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles,new Vector3(viewAngel,0,0),Time.deltaTime*zoomSpeed);
        float preAng = CameraAlign.eulerAngles.z;
        CameraAlign.Rotate(new Vector3(0,0,delta));
            if (CameraAlign.eulerAngles.z < preAng){
                loopCounter++;
            }       
    }

    void SmoothlChangeSpeed(int i){
        currentLoopSpeed = Mathf.Lerp(currentLoopSpeed,loopSpeed[i],Time.deltaTime*changeLoopSpeedFactor);
    }


}
