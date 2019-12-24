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
    public float DefaultMenuCamHeight ;
    public float zoomToLvl=1;
    public float[] loopSpeed;
    public float changeLoopSpeedFactor;
    private  float loopRadius = 1;
    private float rotationStartRadius;
    private float viewAngel;
    public int fastLoopNum ;
    private Vector3 floatPos;
    // public Vector3[] MenuCamPos = new Vector3[2];
    private float radius;
    private int loopCounter;
    private float currentLoopSpeed;
    [SerializeField] private float MenuCamHeight ;


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
                            
                            loopRadius = MapData.MapHeight>MapData.MapWidth ? MapData.MapHeight : MapData.MapWidth;
                            rotationStartRadius = 0.5f*loopRadius;
                                if(loopRadius>9){
                                MenuCamHeight = DefaultMenuCamHeight-(loopRadius-9);
                                }else{MenuCamHeight = DefaultMenuCamHeight;}

                            floatPos=Vector3.zero;
                            transform.localPosition =  new Vector3(0, -loopRadius, 0);

                            CameraAlign.position = target.position+floatPos+new Vector3(0,0,MenuCamHeight);
                            CameraAlign.eulerAngles = Vector3.zero;
                                
                        }
                        ScaleWithFactor(0.8f);
                        transform.LookAt(target.position+floatPos);
                break;
                case LevelManager.lvlState.Play:
                    transform.position = Vector3.Lerp(transform.position,CameraAlign.position, Time.deltaTime * zoomSpeed);
                    // LerpDovodchik(transform.position,CameraAlign.position);
                    HeightChange(PlayCamHeight);
                    ScaleWithFactor(1);
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
                    ScaleWithFactor(0.8f);
                break;

            }

           
    }

    void ScaleWithFactor(float factor){
        // int size = MapData.RotateOn%2==0?MapData.MapWidth:MapData.MapHeight;
        // cam.fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, size*factor, Time.deltaTime * zoomSpeed); 
        int size = MapData.RotateOn%2==0?MapData.MapWidth:MapData.MapHeight;
        // float distance = Mathf.Sqrt(Mathf.Pow(CameraAlign.transform.position.z,2)+Mathf.Pow(loopRadius,2));
        float distance = -CameraAlign.transform.position.z ;
        float MapAspect = (float)(MapData.RotateOn%2==0?MapData.MapWidth:MapData.MapHeight)/(MapData.RotateOn%2==0?MapData.MapHeight:MapData.MapWidth);
        // Debug.Log(Camera.main.aspect+ "  "+ MapAspect);
        float height = size*(1/cam.aspect)+4f;
            if (cam.aspect>MapAspect){
            size = MapData.RotateOn%2==1?MapData.MapWidth:MapData.MapHeight;
                height=size +4;
                            }
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,2*Mathf.Atan(height/2f/distance)/Mathf.PI*180*factor, Time.deltaTime * zoomSpeed); 
        
        // float fov = (-0.505f *Mathf.Pow(size,2) +15.7128f* size +1.0559f);
        // cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,fov, Time.deltaTime * zoomSpeed); 
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
