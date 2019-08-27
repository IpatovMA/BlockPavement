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
    public LevelStageData Stage;
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

    }
    void FixedUpdate()
    {
         if (target == null) return;
  
            Debug.Log(loopCounter);

            switch(LevelManager.State){
                case LevelManager.lvlState.Menu:
                    
                
                        Stage = target.GetComponent<LevelStageData>();
                        viewAngel = transform.eulerAngles.x;
                        loopCounter = 0;

                    if (Stage != null){  
                        floatPos = new Vector3(Stage.MapWidth/2.0f,Stage.MapHeight/2.0f,0);
                        transform.position =  new Vector3(0, -loopRadius, 0)+CameraAlign.position;
                        CameraAlign.position = target.position+floatPos+new Vector3(0,0,MenuCamHeight);
                        CameraAlign.eulerAngles = Vector3.zero;
                        loopRadius = Stage.MapHeight>Stage.MapWidth ? Stage.MapHeight : Stage.MapWidth;
                    }
                
                    ScaleWithFactor(16);
                    transform.LookAt(target.position+floatPos);
                break;
                case LevelManager.lvlState.Play:
                    transform.position = Vector3.Lerp(transform.position,CameraAlign.position, Time.deltaTime * zoomSpeed);
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
                    // makeLoop(loopRadius);
                    // transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, MenuCamPos[1], Time.deltaTime * zoomSpeed);
                    // Debug.Log(currentLoopSpeed);
                    ScaleWithFactor(16);
                    // makeLoop(loopRadius);
                break;

            }

           
    }

 void ScaleWithFactor(float factor){
    //  scaler = Vector3.Lerp(new Vector3(0,0,GetComponent<Camera>().fieldOfView), new Vector3(0,0,Stage.MapWidth*factor), Time.deltaTime * zoomSpeed);
     cam.fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, Stage.MapWidth*factor, Time.deltaTime * zoomSpeed); 
 }
void HeightChange(float height){
    CameraAlign.position=Vector3.Lerp(CameraAlign.position,new Vector3(CameraAlign.position.x,CameraAlign.position.y,height), Time.deltaTime * zoomSpeed);
}

    void makeLoop(float rad){
        // // alpha = Mathf.Lerp(alpha,270,Time.deltaTime*currentLoopSpeed);
        float delta  = Time.deltaTime*currentLoopSpeed;
        // Debug.Log(CameraAlign.localEulerAngles+"  "+CameraAlign.eulerAngles);
        // Debug.Log(Mathf.Pow(transform.localPosition.x,2)+Mathf.Pow(transform.localPosition.y,2)-Mathf.Pow(rad,2));
// Debug.Log(transform.localEulerAngles.x +"  "+ viewAngel);

        // if(rad<loopRadius){
        //     rad = Mathf.Lerp(rad,loopRadius,Time.deltaTime*zoomSpeed);
        //     transform.localPosition = new Vector3(0,rad,0);
        // }
        // if(transform.localEulerAngles.x > viewAngel||transform.localEulerAngles.x<0.1f){
            // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - Time.deltaTime*zoomSpeed*10,0,0);
            if (transform.localEulerAngles.x<0.1f) transform.localEulerAngles = new Vector3(-delta,0,0);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles,new Vector3(viewAngel,0,0),Time.deltaTime*zoomSpeed);
        // }
        CameraAlign.Rotate(new Vector3(0,0,delta));

        if (CameraAlign.eulerAngles.z < delta-delta/10){
            loopCounter++;
            // Debug.Log(loopCounter);
        }
        
    //     if (Mathf.Pow(transform.localPosition.x,2)+Mathf.Pow(transform.localPosition.y,2)-Mathf.Pow(rad,2)<0f){
    //    Debug.Log("dsadsa");
    //         float ang = transform.localEulerAngles.z;
    //         // transform.localPosition = new Vector3(transform.localPosition.x-Mathf.Cos(ang),transform.localPosition.y-Mathf.Sin(ang),0);
    //         transform.Translate(-Mathf.Cos(ang),-Mathf.Sin(ang),0,Space.Self);
    //     }
                

        // // float deltaAng = viewAngel*delta/90f;

        // alpha = 270-alpha>delta ? alpha+delta : -90;

        // float x = target.position.x + floatPos.x + rad*Mathf.Cos(alpha*Mathf.PI/180f);
        // float y = target.position.y + floatPos.y + rad*Mathf.Sin(alpha*Mathf.PI/180f);
        
        // float xAng =transform.eulerAngles.x;
        // float yAng =transform.eulerAngles.y;
        
        // // xAng = alpha<90 ? xAng+deltaAng : xAng-deltaAng;
        // // yAng = alpha>0&&alpha<180 ? yAng+deltaAng : yAng-deltaAng;

        // transform.position = new Vector3(x,y,transform.position.z);
        // transform.eulerAngles = new Vector3(xAng,yAng,alpha+zoomSpeed);
        // // transform.eulerAngles = new Vector3(-viewAngel*Mathf.Sin(alpha*Mathf.PI/180f),viewAngel*Mathf.Cos(alpha*Mathf.PI/180f),alpha+90);

        // // transform.Rotate(new Vector3(deltaAng,-deltaAng,delta));
        // // transform.rotation.Set(xAng,yAng,alpha+90,currentLoopSpeed);
        // // Debug.DrawLine(DebugPos, transform.position, Color.red,60);



    }

    void SmoothlChangeSpeed(int i){
        currentLoopSpeed = Mathf.Lerp(currentLoopSpeed,loopSpeed[i],Time.deltaTime*changeLoopSpeedFactor);
    }

    // void MoveTo(Vector3 pos){
    //     float delta=Time.deltaTime*zoomSpeed; 
    //     if(transform.position != pos){
    //         if(transform.position.x != pos.x){
    //             if (transform.position.x < pos.x) {transform.Translate(delta,0,0);}
    //                 else{transform.Translate(-delta,0,0);}
    //         }
    //         if(transform.position.y != pos.y){
    //             if (transform.position.y < pos.y) {transform.Translate(0,delta,0);}
    //                 else{transform.Translate(0,-delta,0);}
    //         }  
    //         if(transform.position.z != pos.z){ 
    //             if (transform.position.z < pos.z) {transform.Translate(0,0,delta);}
    //                 else{transform.Translate(0,0,-delta);}
    //         }
    //     }
    // }

}

// using UnityEngine;

// // Transform.rotation example.

// // Rotate a GameObject using a Quaternion.
// // Tilt the cube using the arrow keys. When the arrow keys are released
// // the cube will be rotated back to the center using Slerp.

// public class ExampleScript : MonoBehaviour
// {
//     float smooth = 5.0f;
//     float tiltAngle = 60.0f;

//     void Update()
//     {
//         // Smoothly tilts a transform towards a target rotation.
//         float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
//         float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

//         // Rotate the cube by converting the angles into a quaternion.
//         Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

//         // Dampen towards the target rotation
//         transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
//     }
// }

// Quaternion.SetFromToRotation  