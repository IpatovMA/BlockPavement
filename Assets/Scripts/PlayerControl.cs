using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    private Vector2 velocity;
    private Transform RotationSkin;
    void Start()
    {
        // RotationSkin = GetComponentInChildren<BoxCollider2D>().gameObject.transform;
        RotationSkin = transform.Find("align-rotate");
    }

    void Update()
    {   
        velocity = GetComponent<Rigidbody2D>().velocity;
        if(velocity.magnitude<speed/10){velocity = Vector2.zero;}
        if (velocity.x!=0){velocity.y=0;}
        if (velocity.y!=0){velocity.x=0;}


        if (velocity.magnitude>0) {
            SwipeControl.ResetFp();
            // SwipeControl.BlockSwipeInput();
        GetComponent<Rigidbody2D>().velocity = velocity;
            return;
        }
        

        if (Input.GetKeyDown ("w")||SwipeControl.GetUpSwipe())
        {velocity = Vector2.up*speed;
            RotationSkin.eulerAngles= Vector3.forward*90;

        }
        if (Input.GetKeyDown ("s")||SwipeControl.GetDownSwipe())
        {velocity = Vector2.down*speed;
            RotationSkin.eulerAngles= Vector3.forward*(-90);
                    // Debug.Log("down");
        }
        if (Input.GetKeyDown ("a")||SwipeControl.GetLeftSwipe())
        {velocity = Vector2.left*speed;
            RotationSkin.eulerAngles= Vector3.forward*180;
        }
        if (Input.GetKeyDown ("d")||SwipeControl.GetRightSwipe())
        {velocity = Vector2.right*speed;
            RotationSkin.eulerAngles= Vector3.zero;
        }
            // SwipeControl.ResetFp();
        GetComponent<Rigidbody2D>().velocity = velocity;
        // PlayerRotation(velocity);
    }

    void PlayerRotation(Vector2 velocity){
        Vector3 rotate = Vector3.zero;
        switch (velocity.normalized.y){
            case 1: rotate = Vector3.forward;
                break;
            case -1: rotate = Vector3.back;
                break;
        }
        if (velocity.normalized.x == -1){
            rotate = new Vector3(0,0,2);
        }
        RotationSkin.Rotate(90*rotate);

    }
}
