using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float velocitySmoothFactor;
    private Vector2 velocity;
    private float currenFactor=1;
    private float allDistance=0;
    private Vector2 viewDir =Vector2.zero;
    private Transform RotationSkin;
     private float preVelocity=0;

    void Start()
    {
        // RotationSkin = GetComponentInChildren<BoxCollider2D>().gameObject.transform;
        RotationSkin = transform.Find("playeralign");
    }

    void FixedUpdate()
    {   
        velocity = GetComponent<Rigidbody2D>().velocity;
        if(velocity.magnitude<speed/1000||velocity.magnitude>speed*1.2f){
            velocity = Vector2.zero;
        }
        else{
             ChangeFactor(GetDistance(viewDir));
        }
        if (Mathf.Abs(velocity.x)>Mathf.Abs(velocity.y)){velocity.y=0;}
        if (Mathf.Abs(velocity.y)>Mathf.Abs(velocity.x)){velocity.x=0;}

        if(preVelocity!=0&&velocity.magnitude==0&&SwipeControl.AllowSwipes){
            // Invoke( "BlockPaverHelper",0.7f);
            GetComponentInChildren<Animator>().SetTrigger("hited");
        }
        preVelocity= velocity.magnitude;


        if (velocity.magnitude>0) {
            SwipeControl.ResetFp();
            // SwipeControl.BlockSwipeInput();
                    UpdateVelocity(viewDir);
        GetComponent<Rigidbody2D>().velocity = velocity;
            return;
        }
        

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
        
        if(velocity.normalized!= viewDir){
                    RotationSkin.eulerAngles=GetEulerToAlign();
                    SetVelocity(viewDir);
        }        


    }

    public Vector3 GetEulerToAlign(){
        if (viewDir == Vector2.left){
            return Vector3.forward*180;
        }
        if (viewDir == Vector2.up){
            return Vector3.forward*90;
        }
        if (viewDir == Vector2.down){
            return Vector3.forward*(-90);
        }
        // if (viewDir == Vector2.right){
        //     return Vector3.zero;
        // }
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
        if(dist > allDistance*0.5)
        {currenFactor = velocitySmoothFactor + (1-velocitySmoothFactor)*2*(allDistance-dist)/allDistance;
        // Debug.Log("fist "+allDistance+"  "+dist );
        }
        else {
            currenFactor = velocitySmoothFactor + (1-velocitySmoothFactor)*2*dist/allDistance;
        // Debug.Log("snd "+allDistance+"  "+dist );

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
