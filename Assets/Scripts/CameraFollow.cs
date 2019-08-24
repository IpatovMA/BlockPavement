using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 5;
    public float offset = 0f;

    // Update is called once per frame
    void Update()
    {
            // target = LevelSpawner.Level.transform.Find("Player").transform;

         if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset, target.position.y, transform.position.z), Time.deltaTime * smoothTime);
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