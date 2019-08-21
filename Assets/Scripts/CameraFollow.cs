using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public int smoothTime = 5;
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
