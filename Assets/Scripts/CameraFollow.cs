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


    void Start(){
        cam = GetComponent<Camera>();
        // Stage = target.GetComponent<LevelStageData>();
        
    }
    void Update()
    {
        
            // target = LevelSpawner.Level.transform.Find("Player").transform;

         if (target == null) return;
         if (Stage == null){
        Stage = target.GetComponent<LevelStageData>();
         }
                 Vector3 pos = new Vector3(Stage.MapWidth/2,Stage.MapHeight/2,0);

            if (MenuCamera){
                 transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset+pos.x, transform.position.y, transform.position.z), Time.deltaTime * smoothTime);
                scaler = Vector3.Lerp(new Vector3(0,0,GetComponent<Camera>().fieldOfView), new Vector3(0,0,Stage.MapWidth*16), Time.deltaTime * smoothTime);
                cam.fieldOfView = scaler.z; 

            }else{
                transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset+pos.x, target.position.y+pos.y, transform.position.z), Time.deltaTime * smoothTime);
                scaler = Vector3.Lerp(new Vector3(0,0,cam.fieldOfView), new Vector3(0,0,Stage.MapWidth*13), Time.deltaTime * smoothTime);
                cam.fieldOfView = scaler.z; }
        
        // transform.position = Stage.transform.position+pos;
           
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