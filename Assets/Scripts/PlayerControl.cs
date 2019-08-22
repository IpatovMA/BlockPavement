using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    private Vector2 velocity;
    void Start()
    {
        
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
                    // Debug.Log("up");
        }
        if (Input.GetKeyDown ("s")||SwipeControl.GetDownSwipe())
        {velocity = Vector2.down*speed;
                    // Debug.Log("down");
        }
        if (Input.GetKeyDown ("a")||SwipeControl.GetLeftSwipe())
        {velocity = Vector2.left*speed;
                    // Debug.Log("left");
        }
        if (Input.GetKeyDown ("d")||SwipeControl.GetRightSwipe())
        {velocity = Vector2.right*speed;
                            // Debug.Log("rigth");
        }
            // SwipeControl.ResetFp();

        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
