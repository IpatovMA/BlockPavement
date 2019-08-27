using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool MenuCamera=false;
    // public GameObject scaleTarget;
    // public float width;
    public float smoothTime = 5;
    public float offset = 0f;

    private Camera cam;
    private Vector3 scaler;
    public LevelStageData Stage;
    public float PlayCamHeight ;
    private Vector3 floatPos;
    private Vector3 MenuCamPos;
    void Start(){
        cam = GetComponent<Camera>();
    }
    void FixedUpdate()
    {
            // target = LevelSpawner.Level.transform.Find("Player").transform;

         if (target == null) return;
         if (Stage == null||LevelManager.State ==LevelManager.lvlState.Menu){
        Stage = target.GetComponent<LevelStageData>();
              if (Stage != null){  
                floatPos = new Vector3(Stage.MapWidth/2.0f,Stage.MapHeight/2.0f,0);
                 MenuCamPos = transform.position;

                // PlayCamAngels = new Vector3()
              }

         }


            switch(LevelManager.State){
                case LevelManager.lvlState.Menu:
                 transform.position =  new Vector3(target.position.x + offset+floatPos.x, transform.position.y, transform.position.z);
                // MoveTo(new Vector3(target.position.x + offset+floatPos.x, transform.position.y, transform.position.z));
                ScaleWithFactor(16);

                break;
                case LevelManager.lvlState.Play:
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset+floatPos.x, target.position.y+floatPos.y, PlayCamHeight), Time.deltaTime * smoothTime);
                // MoveTo(new Vector3(target.position.x + offset+floatPos.x, target.position.y+floatPos.y, PlayCamHeight));
                // transform.eulerAngles= Vector3.Lerp(transform.eulerAngles, new Vector3(0,0,90), Time.deltaTime * smoothTime);
                ScaleWithFactor(13);

                break;
                case LevelManager.lvlState.Fin:
                transform.position = Vector3.Lerp(transform.position, MenuCamPos, Time.deltaTime * smoothTime);
                ScaleWithFactor(16);
                break;

            }

                transform.LookAt(target.position+floatPos);
           
    }

 void ScaleWithFactor(float factor){
     scaler = Vector3.Lerp(new Vector3(0,0,GetComponent<Camera>().fieldOfView), new Vector3(0,0,Stage.MapWidth*factor), Time.deltaTime * smoothTime);
     cam.fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, Stage.MapWidth*factor, Time.deltaTime * smoothTime); 
 }

    void MoveTo(Vector3 pos){
        float delta=Time.deltaTime*smoothTime; 
        if(transform.position != pos){
            if(transform.position.x != pos.x){
                if (transform.position.x < pos.x) {transform.Translate(delta,0,0);}
                    else{transform.Translate(-delta,0,0);}
            }
            if(transform.position.y != pos.y){
                if (transform.position.y < pos.y) {transform.Translate(0,delta,0);}
                    else{transform.Translate(0,-delta,0);}
            }  
            if(transform.position.z != pos.z){ 
                if (transform.position.z < pos.z) {transform.Translate(0,0,delta);}
                    else{transform.Translate(0,0,-delta);}
            }
        }
    }

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