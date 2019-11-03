﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed=15;
    public float velocitySmoothFactor=0.4f;
    public long VibroMillis= 30;

    private Vector2 velocity;
    public float currenFactor=1;
    private float allDistance=0;
    private Vector2 viewDir =Vector2.zero;
    private Transform RotationSkin;
     private float preVelocity=0;
     private Vector2 preViewDir = Vector2.zero;

    void Start()
    {
        // RotationSkin = GetComponentInChildren<BoxCollider2D>().gameObject.transform;
        RotationSkin = transform.Find("playeralign");
    }

    void FixedUpdate()
    {   
        velocity = GetComponent<Rigidbody2D>().velocity;
        if(velocity.magnitude<speed/1000||velocity.magnitude>speed*2f){
            velocity = Vector2.zero;
        }
        else{
             ChangeFactor(GetDistance(viewDir));
            //  Debug.Log("fac");
        }
        if (Mathf.Abs(velocity.x)>Mathf.Abs(velocity.y)){velocity.y=0;}
        if (Mathf.Abs(velocity.y)>Mathf.Abs(velocity.x)){velocity.x=0;}

        if(preVelocity!=0&&velocity.magnitude==0&&SwipeControl.AllowSwipes){
            // Invoke( "BlockPaverHelper",0.7f);
            RotationSkin.Find("playerscaler").GetComponent<Animator>().SetTrigger("hited");
            //  Debug.Log(RotationSkin.Find("playerscaler").GetComponent<Animator>().gameObject.name);
            Vibration.Vibrate(VibroMillis);
        }
        preVelocity= velocity.magnitude;


        if (velocity.magnitude>0) {
            SwipeControl.ResetFp();
            // SwipeControl.BlockSwipeInput();
                    UpdateVelocity(viewDir);
        GetComponent<Rigidbody2D>().velocity = velocity;
            return;
        }
        
        preViewDir = viewDir;

        if (Input.GetKeyDown ("w")||SwipeControl.GetUpSwipe())
        {
            viewDir = Vector2.up;
            // RotationSkin.eulerAngles= Vector3.forward*90;

        }
        if (Input.GetKeyDown ("s")||SwipeControl.GetDownSwipe())
        {
            viewDir = Vector2.down;
            // RotationSkin.eulerAngles= Vector3.forward*(-90);
        }
        if (Input.GetKeyDown ("a")||SwipeControl.GetLeftSwipe())
        {
            viewDir = Vector2.left;
            // RotationSkin.eulerAngles= Vector3.forward*180;
        }
        if (Input.GetKeyDown ("d")||SwipeControl.GetRightSwipe())
        {
            viewDir = Vector2.right;
            // RotationSkin.eulerAngles= Vector3.zero;
            
        }
            RotationAnim();

        if(velocity.normalized!= viewDir){
                    RotationSkin.eulerAngles=GetEulerToAlign();
                    // Debug.Log(RotationSkin.eulerAngles+"  "+GetEulerToAlign());
                    // Debug.DrawRay(RotationSkin.position,viewDir*10,Color.red,10);
                    SetVelocity(viewDir);
        }        


    }

    void RotationAnim(){
        Debug.Log(Vector3.Angle(preViewDir,viewDir));
        // switch ()
        // {
        //     case 90
        //     default:
        // }
    }

    public Vector3 GetEulerToAlign(){
        
        if (viewDir == Vector2.left){
            RotationSkin.GetComponent<Animator>().SetTrigger("turnBack");
            return Vector3.forward*180;
        }
        if (viewDir == Vector2.up){
            RotationSkin.GetComponent<Animator>().SetTrigger("turnLeft");
            return Vector3.forward*90;
        }
        if (viewDir == Vector2.down){
            RotationSkin.GetComponent<Animator>().SetTrigger("turnRight");
            return Vector3.forward*(-90);
        }
        // if (viewDir == Vector2.right){
        //     return Vector3.zero;
        // }
                // Debug.Log("Eul11");
        return Vector3.zero;

    }
    
    void SetVelocity(Vector2 dir){
        allDistance = GetDistance(viewDir);
        velocity = dir*speed*currenFactor;
        GetComponent<Rigidbody2D>().velocity = velocity;

    }
    void UpdateVelocity(Vector2 dir){
        velocity = dir*speed*currenFactor;
    
    }

    float GetDistance(Vector2 dir){
        Vector3 halfVector = new Vector3(0.5f,0.5f,0);
        RaycastHit2D[] rays = Physics2D.RaycastAll(transform.position+halfVector, dir);
        foreach (var ray in rays)
        {
                if (ray.collider != null&&ray.collider.tag == "border")
            {   
                // border= true;
                Vector3 vect =((Vector3)ray.point - transform.position-halfVector);
                // float distance = Mathf.Round( vect.magnitude-0.5f);
                float distance = ( vect.magnitude-0.5f);

                // Debug.DrawLine(transform.position+halfVector, ray.point,Color.red,2);
                // Debug.Log(distance);
                return distance;
            }    
        }
    return 0;
    }

    void ChangeFactor(float dist){
      
        if (Mathf.Abs(dist)<0.1||Mathf.Abs(allDistance)<0.1) return;
        

        if(dist > allDistance*0.5f)
        {currenFactor = Mathf.Lerp(velocitySmoothFactor,1,(allDistance-dist)/(0.5f*allDistance));
        }
        else  if(dist <allDistance*0.3f){
            currenFactor = Mathf.Lerp(velocitySmoothFactor,1,dist/(allDistance*0.3f));

        }

    }


    // void BlockPaverHelper(){
    //     Vector3 halfVector = new Vector3(0.5f,0.5f,0);
    //     RaycastHit2D[] rays = Physics2D.RaycastAll(transform.position+halfVector, -viewDir);
    //     foreach (var ray in rays)
    //     {
    //                         Debug.DrawLine(transform.position+halfVector, ray.point,Color.red,2);

    //             if (ray.collider != null&&ray.collider.tag == "paved"&&ray.collider.transform.position.z<0)
    //         {   
    //             ray.collider.transform.Translate(0,0,0.01f);
    //             // Debug.DrawLine(transform.position+halfVector, ray.point,Color.red,2);
    //             // Debug.Log(distance);
                
    //         }    
    //     }

    // }

    
    // void OnCollisionEnter2D(Collision2D coll){
                    
    //                 Debug.Log(coll.collider.gameObject.tag);
    //                if(coll.collider.gameObject.tag == "border"){
    //         GetComponentInChildren<Animator>().SetTrigger("hited");

    //         Debug.Log("Hit");
    //     }
    // }
}
